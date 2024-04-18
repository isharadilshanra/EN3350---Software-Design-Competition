package com.project01.restapi.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class FeedbackDetails {

    private Integer Question_Number;
    private String General_Feedback;
    private String Specific_Feedback;


    public FeedbackDetails(
            Integer Question_Number,
            String General_Feedback,
            String Specific_Feedback
    )
    {
        this.Question_Number = Question_Number;
        this.General_Feedback = General_Feedback;
        this.Specific_Feedback = Specific_Feedback;
    }


}
