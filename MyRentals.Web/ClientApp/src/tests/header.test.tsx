import { h } from "preact";
import { render } from "enzyme";
import Header from "../components/header";

describe("Initial Test of the Header", () => {
    test("Header renders 3 nav items", () => {
        const context = render(<Header />);
        expect(context.find("h1").text()).toBe("Preact App");
        expect(context.find("Link").length).toBe(3);
    });
});
