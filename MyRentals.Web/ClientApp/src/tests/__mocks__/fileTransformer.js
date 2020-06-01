/* eslint-disable no-undef */
/* eslint-disable @typescript-eslint/no-var-requires */

const path = require("path");

module.exports = {
    process: (_, filename, __, ___) =>
        "module.exports = " + JSON.stringify(path.basename(filename)) + ";",
};
