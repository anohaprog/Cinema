$(document).ready(
    function () {
        $(".js-seat-container").on("click", ".js-seat-selector",
            function (e) {
                var targetElem = e.currentTarget;
                var dataSet = targetElem.dataset;
                var resultString = 'row:' + dataSet.seatRow + ' col:' + dataSet.seatCol;
                $(".js-seat-result-container").append("<div>" + resultString + "</div>");
            });
    }
);