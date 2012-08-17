//Encapsulates data calls to server (AJAX calls)
var dataService = new function () {
    var serviceBase = '/DataService/',
        getWeatherCondition = function (city, callback) {
            $.getJSON(serviceBase + 'GetWeatherCondition', { city: city }, function (data) {
                callback(data);
        });
    };

    return {
        getWeatherCondition: getWeatherCondition
    };

} ();