import 'fetch';
import 'qs'
import * as extend from 'extend';
import * as appConfig from 'appConfig';
import {autoinject} from 'aurelia-framework';
import {HttpClient, HttpClientConfiguration, Interceptor, RequestInit} from 'aurelia-fetch-client';
import {AuthService} from 'aurelia-authentication';

@autoinject
export class ApiHttpClient {
    private httpClient: HttpClient;
    private authService: AuthService;

    private defaultRequestInit: RequestInit = {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    };

    constructor(httpClient: HttpClient, authService: AuthService) {
        this.httpClient = httpClient;
        this.authService = authService;

        this.httpClient.configure(x => this.configureHttpClient(x));
    }

    public get<TResult>(resourcePath: string, query, options): Promise<TResult> {
        return this.request<TResult>('GET', resourcePath, query, undefined, options);
    }

    public post(resourcePath: string, body, options): Promise<any> {
        return this.request('POST', resourcePath, undefined, body, options);
    }

    public update(resourcePath: string, query, body, options): Promise<any> {
        return this.request('PUT', resourcePath, query, body, options);
    }

    public delete(resourcePath: string, query, options): Promise<any> {
        return this.request('DELETE', resourcePath, query, undefined, options);
    }

    private request<TResult>(method: string, resourcePath: string, query, body, options: RequestInit = {}): Promise<TResult> {
        let resourcePathTmp = resourcePath;
        if (typeof query === 'object')
            resourcePathTmp += "?" + QueryString.stringify(query);

        let requestInitTmp: RequestInit = {
            method: method
        };

        if (typeof body === 'object')
            requestInitTmp.body = JSON.stringify(body);

        let requestInit = extend(true, {}, this.defaultRequestInit, requestInitTmp);

        return this.httpClient.fetch(resourcePathTmp, requestInit).then(response => {
            if (response.status >= 200 && response.status < 400) {
                return response.json().catch(error => null);
            }

            throw response;
        });
    }

    private configureHttpClient(config: HttpClientConfiguration) {
        config.withBaseUrl(appConfig.ApiBaseUrl)
            .rejectErrorResponses()
            .withInterceptor(this.getInterceptor());
    }

    private getInterceptor(): Interceptor {
        return {
            request: request => {
                // TODO: add log service
                console.log(`Requesting ${request.method} ${request.url}`);
                
                if (!this.authService.isAuthenticated())
                    return request;

                let token = this.authService.getAccessToken();

                request.headers.set("Authorization", `Bearer ${token}`);
                return request;
            },
            response: (response, request) => {
                console.log(`Received ${response.status} ${response.url}`);
                
                return new Promise((resolve, reject) => {
                    if (response.ok) {
                        return resolve(response);
                    }

                    reject(response);
                });
            }
        };
    }
}