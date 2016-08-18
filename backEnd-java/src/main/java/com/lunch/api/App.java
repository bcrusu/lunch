package com.lunch.api;

import com.lunch.api.controllers.TestController1;
import com.lunch.api.controllers.TestController2;
import org.springframework.boot.Banner;
import org.springframework.boot.SpringApplication;

public class App {
    public static void main(String[] args) throws Exception {
        SpringApplication app = new SpringApplication(TestController1.class, TestController2.class);
        app.setWebEnvironment(true);
        app.setBannerMode(Banner.Mode.OFF);
        app.run(args);
    }
}
