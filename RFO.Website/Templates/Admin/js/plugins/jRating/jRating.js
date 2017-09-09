(function ($) {
    $.fn.rating = function (options) {
        var container = $(this);
        
        // Init default values
        var settings = $.extend({
            maxvalue: 5,
        }, options);
        
        // Get extend description
        $.extend(settings, {
            defaultDescription: '',
        });
        
        $.extend(settings, {
            displayDescription: settings.defaultDescription
        });
        
        // Init fieldset
        var $fieldset = $('<fieldset class="star-rating-widget"></fieldset>')
            .appendTo(container);
            
        // Init ul stars
        var $ul = $('<ul class="stars-0"></ul>')
            .appendTo($fieldset);
            
        // Init li stars
        for (var i = 1; i <= settings.maxvalue; i++) {
            var $li = $('<li><input id="rating-' + i + '" type="radio" name="rating" value="' + i + '"></li>')
                .appendTo($ul);
        }
        
        // Get elements that the class attribute is begined with rating
        var stars = $(container).find('input[id^="rating"]');
        
        // Handle mouse event
        stars
            .mouseover(function () {
                event.drain();
                event.fill(this);
            })
            .mouseout(function () {
                event.drain();
                event.reset();
            });
        
        // Handle click on star
        stars.click(function () {
            // The index is begin with 0, so increase index by 1
            settings.curvalue = stars.index(this) + 1;

            // Handle update Rating description and value at here
            $.publish("SetRatingPointEvent", [settings.curvalue]);

            return true;
        });
        
        // Handle events
        var event = {
            // The input of fill function is the element that is activated
            fill: function (activeItem) {
                // The index is begin with 0, so increase index by 1
                var index = stars.index(activeItem) + 1;
                $ul.removeClass().addClass('stars-' + index);
            },
            drain: function () { // drain all the stars.
                $ul.removeClass().addClass('stars-0');
            },
            reset: function () { // Reset the stars to the default index.
                $ul.removeClass().addClass('stars-' + settings.curvalue);
            }
        };

        /**
         * Load rating point when starting
         */
        var onLoad = function () {
            event.drain();
            event.reset();
        };
        onLoad();
    };
} (jQuery));