package com.project01.restapi.config;

import com.project01.restapi.service.UserService;
import org.modelmapper.ModelMapper;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class AppConfig {

@Bean
    ModelMapper getMapper(){
        return new ModelMapper();
}
}
