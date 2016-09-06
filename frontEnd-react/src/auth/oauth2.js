import url from 'url';
import qs from 'querystring';
import api from '../api'

export function linkedinSignin() {
    const config = {
        apiFunction: api.apiFunctions.signInLinkedIn,
        clientId: '77d08hsurfp6gr',
        redirectUri: 'http://localhost:3000',  //TODO: move to config section
        authorizationUrl: 'https://www.linkedin.com/uas/oauth2/authorization',
        scope: 'r_emailaddress r_basicprofile',
        popupOptions: { width: 527, height: 582 }
    };

    return oauth2(config)
        .then(openPopup)
        .then(pollPopup)
        .then(exchangeCodeForToken)
        .then(closePopup)
}

function oauth2(config) {
    return new Promise((resolve, reject) => {
        const params = {
            client_id: config.clientId,
            redirect_uri: config.redirectUri,
            scope: config.scope,
            response_type: 'code',
            state: 'dummy_state'  //TODO: proper CSRF token            
        };
        const url = config.authorizationUrl + '?' + qs.stringify(params);
        resolve({ url, config });
    });
}

function openPopup({ url, config }) {
    return new Promise((resolve, reject) => {
        const popupOptions = config.popupOptions
        const width = popupOptions.width || 500;
        const height = popupOptions.height || 500;

        const popupSpecs = {
            width: width,
            height: height,
            top: window.screenY + ((window.outerHeight - height) / 2.5),
            left: window.screenX + ((window.outerWidth - width) / 2)
        };
        const popup = window.open(url, '_blank', qs.stringify(popupSpecs, ','));

        resolve({ window: popup, config });
    });
}

function pollPopup({ window, config }) {
    return new Promise((resolve, reject) => {
        const redirectUri = url.parse(config.redirectUri);
        const redirectUriPath = redirectUri.host + redirectUri.pathname;

        const interval = setInterval(() => {
            if (!window || window.closed) {
                clearInterval(interval);
                reject({ error: 'User cancelled.' })
            }
            try {
                const popupUrlPath = window.location.host + window.location.pathname;
                if (popupUrlPath === redirectUriPath) {
                    if (window.location.search || window.location.hash) {
                        const query = qs.parse(window.location.search.substring(1).replace(/\/$/, ''));
                        const hash = qs.parse(window.location.hash.substring(1).replace(/[\/$]/, ''));
                        const params = Object.assign({}, query, hash);

                        //TODO: validate params.state (CSRF token)

                        if (params.error) {
                            reject({ error: params.error })
                        } else {
                            resolve({ oauthData: params, config, window });
                        }
                    } else {
                        reject({ error: 'OAuth redirect has occurred but no query or hash parameters were found.' })
                    }

                    clearInterval(interval)
                }
            } catch (error) {
                // Ignore DOMException: Blocked a frame with origin from accessing a cross-origin frame.
                // A hack to get around same-origin security policy errors in Internet Explorer.
            }
        }, 500);
    });
}

function exchangeCodeForToken({ oauthData, config, window }) {
    const data = {
        code: oauthData.code,
        redirectUri: config.redirectUri
    }

    return config.apiFunction(data)
        .then(json => {
            return { token: json.token, window }
        })
}

//TODO: close popup on error (reject)
function closePopup({ token, window }) {
    return new Promise((resolve, reject) => {
        window.close();
        resolve(token);
    });
}
