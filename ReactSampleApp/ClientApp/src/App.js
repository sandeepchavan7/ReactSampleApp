import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { AuthorizedUserLayout } from './components/AuthorizedUserLayout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { LoginComponent } from './components/user/LoginComponent';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        const isAuthorized = sessionStorage.getItem('session_token') === null ? false : true;
        return (
            <div>
                {
                    isAuthorized === true ?
                        <AuthorizedUserLayout>
                            <Route path='/home' component={Home} />
                            <Route path='/counter' component={Counter} />
                            <Route path='/fetch-data' component={FetchData} />
                        </AuthorizedUserLayout>
                        : <Layout>
                            <Route exact path='/' component={LoginComponent} />
                            <Route path='/login' component={LoginComponent} />
                        </Layout>
                }
            </div>
        );
    }
}
