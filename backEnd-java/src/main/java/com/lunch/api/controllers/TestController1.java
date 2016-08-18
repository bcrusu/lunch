package com.lunch.api.controllers;

import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@EnableAutoConfiguration
@RestController
public class TestController1 {
    @RequestMapping("/")
    String home() {
        return "Hello World!";
    }
}
