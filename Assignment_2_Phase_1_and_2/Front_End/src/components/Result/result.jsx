import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import './result.css'; 

const ResultPage = () => {
    const [userAnswers, setUserAnswers] = useState([]);
    const { userIndex } = useParams();

    useEffect(() => {
        // Assume userAnswers is retrieved from the backend after submitting the quiz
        const fetchUserAnswers = async () => {
            // Fetch user answers based on userIndex from local storage or backend
            const storedUserAnswers = localStorage.getItem(`userAnswers_${userIndex}`);
            if (storedUserAnswers) {
                setUserAnswers(JSON.parse(storedUserAnswers));
            } else {
                console.error('User answers not found.');
            }
        };

        fetchUserAnswers();
    }, [userIndex]);

    return (
        <div className="result-container">
            <h2 className="result-heading">Quiz Results</h2>
            <div className="user-answers-container">
                {userAnswers.map((answer, index) => (
                    <div className="user-answer-item" key={index}>
                        <h3 className="question-num">Question {index + 1}</h3>
                        <p className="question">Question: {answer.question}</p>
                        <p className="user-answer">Your Answer: {answer.selectedOption}</p>
                        <p className={`feedback ${answer.isCorrect ? 'correct' : 'incorrect'}`}>
                            {answer.isCorrect ? 'Correct!' : 'Incorrect!'} {answer.feedback}
                        </p>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ResultPage;

