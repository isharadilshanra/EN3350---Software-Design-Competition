package com.project01.restapi.dao;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Entity
@Getter
@Setter
public class Answers {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer Question_Number;
    private String General_Feedback;
    private String Specific_Feedback_a;
    private String Specific_Feedback_b;
    private String Specific_Feedback_c;
    private String Specific_Feedback_d;
    private Integer Answer;

}
