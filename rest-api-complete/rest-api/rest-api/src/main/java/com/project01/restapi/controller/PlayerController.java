package com.project01.restapi.controller;
import com.project01.restapi.dto.*;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RestController;

import com.project01.restapi.dao.PlayerEmail;
import com.project01.restapi.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins = "http://localhost:5173")
@RestController
@RequestMapping("Players")
public class PlayerController {

//    variable , if use @AutoWired that means it creates an instance as well, no need for separate instance creation
//    @Service term should be in UserService, it will create a bean from the UserService class
    @Autowired
    UserService userService;


//    construct an instance of UserService if Autowired does not use

    @PostMapping("players")
    void createPlayer(@RequestBody PlayerDetails playerDetails){
        userService.addPlayer(playerDetails);
    }

    @PostMapping("uploadEmails")
    void updatePlayerEmail(@RequestBody String email){
        userService.updatePlayerEmail(email);
    }

    @CrossOrigin(origins = "http://localhost:5173")
    @PostMapping("uploadAnswers")
    void updatePlayerAnswers(@RequestBody int[] answers){
        userService.updatePlayerAnswers(answers);
    }

    @PostMapping("updateScores")
    void createPlayerScore(@RequestBody ScoreDetails scoreDetails){
        userService.addScore(scoreDetails);
    }

    @GetMapping("getPlayerDetails")
    List<PlayerDetails> getAllPlayers(){return userService.getAllPlayers();
    }

    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping("getAnswerFeedback")
    List<FeedbackDetails> getAllAnswers() {return userService.getAllAnswers(); }

    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping("getScores")
    AllScoreDetails getAllScores() {return userService.getAllScores(); }
    @PostMapping("uploadDBAnswers")
    void createAnswer(@RequestBody AnswersDetails answersDetails) {userService.addAnswer(answersDetails); }

}

