import {autoinject} from 'aurelia-framework';
import * as router from 'aurelia-router'
import {AuthService} from 'aurelia-authentication';

@autoinject
export class SignIn {
  private authService: AuthService;
  private router: router.Router;

  constructor(authService: AuthService, router: router.Router) {
    this.authService = authService;
    this.router = router;
  };

  get authenticated() {
    return this.authService.isAuthenticated();
  }

  authenticateLinkedin() {
    return this.authService.authenticate('linkedin')
      .then(() => {
        return;
      });
  }
}
