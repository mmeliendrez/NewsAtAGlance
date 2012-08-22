//Contains the initial screen startup routines
var startup = function () {
    var windowFocused = true,
    init = function (city) {
        
        //track if user switches tabs or not otherwise
        //timers may queue up in some browsers like Chrome
        $(window).focus(function () {
            windowFocused = true;
        });

        $(window).blur(function () {
            windowFocused = false;
        });
        
        var defaultPositions = sceneLayoutService.get();

        $('#gridButton').click(function () {
            sceneStateManager.changeScene();
        });

        $('#cloudButton').click(function () {
            sceneStateManager.changeScene();
        });
        
        sceneStateManager.init(defaultPositions);
        
        sceneStateManager.renderTiles(city);

        setTimeout(function () {
            
            $("#aaglogo").delay('500').fadeIn('slow');

            $('.tile').each(function () {
                $(this)./*delay(Math.floor(Math.random() * 450)).*/fadeIn(360, 'easeInCubic', function () {
                    
                });
            });

        }, 1000);

        //Update weatherCondition data on timer basis
        setInterval(function () {
            if (!windowFocused) return;
            dataService.getWeatherCondition(city, renderWeatherTiles);
        }, 15000);

        var sceneChangeTimer = setInterval(function () {
            
        }, 2000);
    },

    renderWeatherTiles = function (json) {
        $('div.tile[id^="WeatherCondition"]').each(function () {
            sceneStateManager.renderTile(json, $(this), 500);
        });
    };

    return {
        init: init
    };
} ();

