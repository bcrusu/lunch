import {autoinject} from 'aurelia-framework';
import * as router from 'aurelia-router'
import {AuthService} from 'aurelia-authentication';

export class App {
  private router: router.Router;

  configureRouter(config: router.RouterConfiguration, router: router.Router) {
    config.title = 'Lunch';
    config.addAuthorizeStep(AuthorizeStep);
    config.map([
      { route: ['', 'signin'], name: 'signin', moduleId: 'signin', nav: true, title: 'Sign in' },
      { route: ['app'], name: 'app', moduleId: 'app/appRoot', nav: true, title: 'Welcome' },
    ]);

    this.router = router;
  }
}

@autoinject
class AuthorizeStep implements router.PipelineStep {
  private authService: AuthService;

  constructor(authService: AuthService) {
    this.authService = authService;
  }

  run(navigationInstruction: router.NavigationInstruction, next: router.Next) {  
    if (!navigationInstruction.fragment.startsWith('/app'))
      return next();
    
    var isAuthenticated = this.authService.isAuthenticated();
    
    if (!isAuthenticated) {
      return next.cancel(new router.Redirect('signin'));
    }

    return next();
  }
}