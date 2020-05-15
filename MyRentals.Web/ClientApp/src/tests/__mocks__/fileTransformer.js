// eslint-disable-next-line @typescript-eslint/no-var-requires
// eslint-disable-next-line no-undef
// eslint-disable-next-line @typescript-eslint/no-var-requires
const path = require("path");

// eslint-disable-next-line no-undef
module.exports = {
    process: (_, filename, __, ___) =>
        "module.exports = " + JSON.stringify(path.basename(filename)) + ";",
};
