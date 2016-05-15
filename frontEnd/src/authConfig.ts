let baseConfig = {
    baseUrl: 'http://localhost:7777/api',  // API base address
    endpoint: null,  // use Aurelia HttpClient instead of 'aurelia-api' client
    configureEndpoints: [],
    useRefreshToken: false,
    autoUpdateToken: false,
    
    // TODO: review 4 properties below
    loginRedirect: '#/customer',
    logoutRedirect: '/home',
    loginRoute: '/login',
    loginOnSignup: true,
    
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