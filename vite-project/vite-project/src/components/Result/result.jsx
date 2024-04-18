import React, { useState, useEffect } from 'react';
import './result.css'; 
import axios from 'axios';
import backgroundVideo from '../../../Videos/quiz_results.mp4';

const QuizResultPage = () => {
    const [feedbackData, setFeedbackData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                console.log("Fetching quiz results...");
                const response = await axios.get('http://localhost:8080/Players/getAnswerFeedback');
                console.log("Quiz results fetched successfully.");
                setFeedbackData(response.data);
            } catch (error) {
                console.error('Error fetching quiz results:', error);
            }
        };

        fetchData();
    }, []);

    return (
        <div className="result-container">
            <div className="background">
                <video autoPlay loop muted className='video-player'>
                    <source src={backgroundVideo} type='video/mp4' />
                </video>
            </div>
            <h2 className="result-header">Quiz Results</h2>
            <div className="feedback-section">
                {feedbackData.map((item, index) => (
                    <div className="feedback-item" key={index}>
                        <h3 className="question-number">Question {item.question_Number}</h3>
                        <p className="general-feedback">{item.general_Feedback}</p>
                        <p className="specific-feedback">{item.specific_Feedback}</p>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default QuizResultPage;