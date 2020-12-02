import React, { Component } from 'react';
import { Container } from 'reactstrap';

export class AuthorizedUserLayout extends Component {
    static displayName = GuestLayout.name;

    render() {
        return (
            <div>
                <NavMenu />
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
