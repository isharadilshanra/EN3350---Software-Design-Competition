package com.project01.restapi.service;

import com.project01.restapi.dto.*;
import com.project01.restapi.dao.Score;
import com.project01.restapi.repository.ScoreRepository;


import com.project01.restapi.dao.Player;
import com.project01.restapi.repository.PlayerRepository;

import com.project01.restapi.dao.Answers;
import com.project01.restapi.repository.AnswersRepository;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class UserService {
    @Autowired
    PlayerRepository playerRepository;

    @Autowired
    AnswersRepository answersRepository;

    @Autowired
    ScoreRepository scoreRepository;

    @Autowired
    ModelMapper mapper;

    //  Loading Player Details from database
    public List<PlayerDetails> getAllPlayers() {
        //TODO load all system Players
        List<PlayerDetails> playerDetails = new ArrayList<>();
        playerRepository.findAll().forEach((player)->{
            //  converting player dao entity to dto, (dto - client side, dao - DB side)
            playerDetails.add(this.mapper.map(player, PlayerDetails.class));
        });
        return playerDetails;
    }

    public AllScoreDetails getAllScores() {
        //TODO load all Players Scores
        List<ScoreDetails> scoreDetails = new ArrayList<>();
        scoreRepository.findAll().forEach((score)->{
            //  converting player dao entity to dto, (dto - client side, dao - DB side)
            scoreDetails.add(this.mapper.map(score, ScoreDetails.class));
        });
        AllScoreDetails allscoredetails = new AllScoreDetails();
        allscoredetails.setPlayers(scoreDetails);

        return allscoredetails;
    }

    //  Add values to DB
    public  void addPlayer(PlayerDetails playerDetails){
        //  converting the input dto entity to dao model
        this.playerRepository.save(mapper.map(playerDetails, Player.class ));
    }

    // Method to update only the email
    public void updatePlayerEmail(String newEmail) {
        PlayerDetails player = new PlayerDetails();       // Update Player details
        player.setEmail(newEmail);
        addPlayer(player);
    }

    public void updatePlayerAnswers(int[] answers) {
        List<PlayerDetails> playerList = getAllPlayers();
        if (!playerList.isEmpty()) {
            PlayerDetails player = playerList.get(playerList.size() - 1);
            player.setAnswers(answers);
            // Assuming addPlayer is supposed to replace the last player with the updated one
            addPlayer(player);
            //player.setScore(score);
            player.setAnswers(answers);
            addPlayer(player);
        }
    }

    // Method to update only the answers array
//    public void updatePlayerAnswers(long playerId, int[] newAnswers) {
//        Optional<Player> optionalPlayer = playerRepository.findById(playerId);
//        optionalPlayer.ifPresent(player -> {
//            player.setAnswers(newAnswers);
//            playerRepository.save(player);
//        });
//    }


    public  void addScore(ScoreDetails scoreDetails){
        //  converting the input dto entity to dao model
        this.scoreRepository.save(mapper.map(scoreDetails, Score.class ));
    }

    // Creating feedback with all the answers
    public List<FeedbackDetails> getAllAnswers() {
        List<AnswersDetails> answersDetails = new ArrayList<>();
        answersRepository.findAll().forEach((answer) -> {
            answersDetails.add(this.mapper.map(answer, AnswersDetails.class));

        });

        // Create new variables of required data types
        List<FeedbackDetails> feedbackDetails = new ArrayList<>();

        List<PlayerDetails> currentPlayerList = getAllPlayers();
        int playerCount                       = currentPlayerList.size();                       // Calculating number of players in the DB
        PlayerDetails currentPlayer           = currentPlayerList.get(playerCount - 1);         // Get the last player of DB as current player

        //int score             = currentPlayer.getScore();
        String email          = currentPlayer.getEmail();
        int[] selectedAnswers = currentPlayer.getAnswers();    // Get answers of current player
        //System.out.println(currentPlayer.getName());
        int score;

        Score records = scoreRepository.findByEmail(email);
        score = 0 ;
//        if (records == null){   // New user | Not previously attempted
//            score = 0;
//        }
//        else {                  // Existing User
//            score = records.getScore();
//            return new ArrayList<FeedbackDetails>();

//        }

        for (int i = 0; i < Math.min(answersDetails.size(), 10); i++){
            //Set selected feedbacks for given question
            FeedbackDetails feedback = new FeedbackDetails();
            feedback.setGeneral_Feedback(answersDetails.get(i).getGeneral_Feedback());
            Integer currentAnswer = answersDetails.get(i).getAnswer();
            AnswersDetails playerAnswers = answersDetails.get(i);
            switch (selectedAnswers[i]) {      // Filter out specific feedback
                case 0:
                    feedback.setSpecific_Feedback(playerAnswers.getSpecific_Feedback_a());
                    break;
                case 1:
                    feedback.setSpecific_Feedback(playerAnswers.getSpecific_Feedback_b());
                    break;
                case 2:
                    feedback.setSpecific_Feedback(playerAnswers.getSpecific_Feedback_c());
                    break;
                case 3:
                    feedback.setSpecific_Feedback(playerAnswers.getSpecific_Feedback_d());
                    break;
                default:
                    // Handle the case where selected answer is not 1, 2, 3, or 4
                    break;
            }
            try {
                if (selectedAnswers[i] == currentAnswer) {
                    score += 10;
                }
            }
            catch(Exception err){
                System.out.println(err.getMessage());
            }

            //System.out.println(selectedAnswers[i]);
            feedback.setQuestion_Number(answersDetails.get(i).getQuestion_Number());
            feedbackDetails.add(feedback);
        }
        //playerRepository.delete(currentPlayer);

        ScoreDetails newScore = new ScoreDetails();      //Update score fields
        newScore.setEmail(email);
        newScore.setScore(score);
        newScore.setAttempted(true);
        addScore(newScore);


        PlayerDetails player = new PlayerDetails();       // Update Player details
        //player.setScore(score);
        player.setAnswers(selectedAnswers);
        player.setEmail(email);
        player.setId(currentPlayer.getId());
        addPlayer(player);

        return feedbackDetails;

    }

    public void addAnswer(AnswersDetails answersDetails){
        this.answersRepository.save(mapper.map(answersDetails, Answers.class));
    }

}