package com.project01.restapi.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class AnswersDetails {

    private Integer Question_Number;
    private String General_Feedback;
    private String Specific_Feedback_a;
    private String Specific_Feedback_b;
    private String Specific_Feedback_c;
    private String Specific_Feedback_d;
    private Integer Answer;

    public AnswersDetails(
            Integer question_Number,
            String general_Feedback,
            String specific_Feedback_a,
            String specific_Feedback_b,
            String specific_Feedback_c,
            String specific_Feedback_d,
            Integer answer
    )
    {
        this.Question_Number = question_Number;
        this.General_Feedback = general_Feedback;
        this.Specific_Feedback_a = specific_Feedback_a;
        this.Specific_Feedback_b = specific_Feedback_b;
        this.Specific_Feedback_c = specific_Feedback_c;
        this.Specific_Feedback_d = specific_Feedback_d;
        this.Answer = answer;

    }


}
