import {Router, RouterConfiguration} from 'aurelia-router'

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Lunch';
    config.map([
      { route: ['', 'login'], name: 'login', moduleId: 'public/login', nav: true, title: 'Login' },
    ]);

    this.router = router;
  }
}
