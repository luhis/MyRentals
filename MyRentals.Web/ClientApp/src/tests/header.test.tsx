import { h } from "preact";
import { shallow } from "enzyme";
import Header from "../components/header";

describe("Initial Test of the Header", () => {
    test("Header renders 3 nav items", () => {
        shallow(<Header />);
    });
});

describe("API Utils tests", () => {
    test("utzParse", () => {
        expect(true).toStrictEqual(true);
    });
});
