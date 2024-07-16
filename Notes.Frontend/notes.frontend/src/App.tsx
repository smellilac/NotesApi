import React, { FC, ReactElement } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css'; 

import userManager, { loadUser, signinRedirect } from './auth/user-service';
import AuthProvider from './auth/auth-provider';
import SignInOidc from './auth/SignInOidc';
import SignOutOidc from './auth/SignOutOidc';
import NoteList from './notes/NoteList';

const App: FC = (): ReactElement => {
    loadUser();
    console.log('App component loaded'); // Лог для проверки загрузки компонента

    return (
        <div className="App">
            <header className="App-header">
                <button onClick={() => signinRedirect()}>Login</button>
                <AuthProvider userManager={userManager}>
                    <Router>
                        <Routes>
                            <Route path="/" element={<NoteList />} />
                            <Route path="/signout-oidc" element={<SignOutOidc />} />
                            <Route path="/signin-oidc" element={<SignInOidc />} />
                        </Routes>
                    </Router>
                </AuthProvider>
            </header>
        </div>
    );
};

export default App;
