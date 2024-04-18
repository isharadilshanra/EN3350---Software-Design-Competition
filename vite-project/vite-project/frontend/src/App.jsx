import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import StartQuiz from './StartQuiz';
import Quiz from './components/Quiz/quiz';
import Result from './components/Result/result';

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<StartQuiz />} />
          <Route path="/quiz" element={<Quiz />} />
          <Route path="/result" element={<Result />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;


