import {Router, RouterConfiguration} from 'aurelia-router'

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Lunch';
    config.map([
      { route: ['', 'signin'], name: 'signin', moduleId: 'public/signin', nav: true, title: 'Sign in' },
    ]);

    this.router = router;
  }
}
