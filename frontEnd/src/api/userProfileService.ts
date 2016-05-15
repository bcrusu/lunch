import {autoinject} from 'aurelia-framework';
import {ApiHttpClient} from 'api/apiHttpClient'

@autoinject
export class UserProfileService {
    apiHttpClient: ApiHttpClient;

    constructor(apiHttpClient: ApiHttpClient) {
        this.apiHttpClient = apiHttpClient;
    }

    public getUserInfo(): Promise<UserInfo> {
        return this.apiHttpClient.get<UserInfo>('/userProfile/userInfo', undefined, {});
    }
}

export interface UserInfo {
    displayName: string,
    smallPictureUrl: string
}