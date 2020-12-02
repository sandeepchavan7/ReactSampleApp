import React, { Component } from 'react';

export class LoginComponent extends Component {
    static displayName = LoginComponent.name;

    constructor(props) {
        super(props);
        this.state = { userName: '', password: '', isSubmited: false, message: '' };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    async handleSubmit(event) {
        event.preventDefault();
        this.setState({ isSubmited: true });
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ userName: this.state.userName, password: this.state.password })
        };
        const response = await fetch('authentication', requestOptions);
        const data = await response.json();
        if (data.isSuccess) {
            sessionStorage.setItem("session_token", data.token);
            window.location.href = "/home";
        } else {
            this.setState({ isSubmited: false, message: data.message });
		}
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div>
                    <label>
                        UserName:
                        <input type="text" name="userName" value={this.state.userName} onChange={this.handleInputChange} disabled={this.state.isSubmited} />
                    </label>
                </div>
                <div>
                    <label>
                        Password:
                        <input type="password" name="password" onChange={this.handleInputChange} disabled={this.state.isSubmited} />
                    </label>
                </div>
                <div>
                    <label>{this.state.message}</label>
                </div>
                <input type="submit" value="Submit" disabled={this.state.isSubmited}/>
            </form>
        );
    }
}
