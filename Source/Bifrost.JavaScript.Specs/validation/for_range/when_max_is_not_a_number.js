﻿describe("when not specifying max", function () {
    var exception = null;
    try {
        var validator = Bifrost.validation.range.create({ options: { min: 5, max: "MAX" } });
        validator.validate("1234");
    } catch (e) {
        exception = e;
    }
    it("should throw an exception", function () {
        expect(exception instanceof Bifrost.validation.NotANumber).toBe(true);
    });
});