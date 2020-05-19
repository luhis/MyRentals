import { FunctionalComponent, h } from "preact";
import { Navbar, Button } from "rbx";

import { useGoogleAuth, useAccess } from "../app";
import { getAccess } from "../../api/access";

const Header: FunctionalComponent = () => {
    const { signIn } = useGoogleAuth();
    const [access, setAccess] = useAccess();
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
                    {access.canViewRealtors ? (
                        <Navbar.Item href="/realtors">Realtors</Navbar.Item>
                    ) : null}
                </Navbar.Segment>
            </Navbar.Menu>
            <Button
                onClick={async (): Promise<void> => {
                    const user = await signIn();
                    if (user) {
                        setAccess(await getAccess(user.tokenId));
                    } else {
                        console.log(user);
                    }
                }}
            >
                Sign in with Google
            </Button>
        </Navbar>
    );
};

export default Header;
