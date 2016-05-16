import * as appConfig from 'appConfig'

let baseConfig = {
    baseUrl: appConfig.ApiBaseUrl,
    endpoint: null,  // use Aurelia HttpClient instead of 'aurelia-api' client
    configureEndpoints: [],
    loginOnSignup: true,
    useRefreshToken: false,
    autoUpdateToken: false,
    
    loginRedirect: '#/app',
    logoutRedirect: '#/signin',
        
    providers: {
        linkedin: {
            url: 'Account/SignInLinkedin',
            clientId: '77d08hsurfp6gr',
            scope: ['r_emailaddress', 'r_basicprofile'],
            state: 'll'
        }
    }
};

export default baseConfig;