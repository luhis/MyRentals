import { FunctionalComponent, h } from "preact";
import { Navbar, Button } from "rbx";
import { useGoogleAuth } from "../app";

const Header: FunctionalComponent = () => {
    const { signIn } = useGoogleAuth();
    return (
        <Navbar>
            <Navbar.Brand>
                <Navbar.Item href="#">
                    <img
                        src="https://bulma.io/images/bulma-logo.png"
                        alt=""
                        role="presentation"
                        width="112"
                        height="28"
                    />
                </Navbar.Item>
                <Navbar.Burger />
            </Navbar.Brand>
            <Navbar.Menu>
                <Navbar.Segment align="start">
                    <Navbar.Item href="/">Home</Navbar.Item>
                    <Navbar.Item href="/apartments">Apartments</Navbar.Item>
                    <Navbar.Item href="/clients">Clients</Navbar.Item>
                    <Navbar.Item href="/realtors">Realtors</Navbar.Item>
                </Navbar.Segment>
            </Navbar.Menu>
            <Button
                onClick={(): void => {
                    signIn();
                }}
            >
                Sign in with Google
            </Button>
        </Navbar>
    );
};

export default Header;
