import * as router from 'aurelia-router'

export class Root {
  private router: router.Router;

  configureRouter(config: router.RouterConfiguration, router: router.Router) {
    config.title = 'Welcome';    
    config.map([
      { route: ['', 'welcome'], name: 'welcome', moduleId: 'app/welcome', nav: true, title: 'Welcome' },
    ]);

    this.router = router;
  }
}
