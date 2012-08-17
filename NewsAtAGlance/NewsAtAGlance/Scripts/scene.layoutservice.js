//Contains tile size and scene layout details
var sceneLayoutService = function () {
    var width,
        get = function () {
            width = $('#content').width();

            //scene1
            var pad = 6,
            r1H = 210,
            //small
            s1Sh = 93,
            s1Sh2 = 93,
            s1Sw = 264,
            //medium
            s1Mh = 197,
            s1Mw = 365,
            s1Mw2 = 270,
            //large
            s1Lh = 340,
            s1Lw = 584,

            items = { tiles:
                    [
                    { name: 'Weather Condition',
                        tileId: 'WeatherCondition',
                        scenes: [
                            { height: s1Mh, width: s1Mw, top: 0, left: 0, opacity: 1, size: 1, borderColor: '#5E1B6B', z: 0 },
                            { height: 90, width: 210, top: 80, left: 250, size: 0, borderColor: '#5E1B6B', z: '2000', opacity: .5 }
                        ]
                    }
                     ]
                };

        return items;
    };
    
    return {
        get: get
    };

} ();