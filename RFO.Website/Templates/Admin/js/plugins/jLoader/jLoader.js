(function ($) {
    $.fn.showLoader = function (options) {
        var self = this;
        var settings = $.extend({
            // These are the defaults, it will be completely overwritten 
            // by a property with the same key in the second or subsequent object
            loaderContainer: 'loader',
            loaderImg: '../Templates/Admin/images/processing.gif',
            left: $(self).position().left - 64,
            top: 0
        }, options);

        // Return the active loader or creates a new loader
        var getLoader = function () {
            loader = $('#' + settings.loaderContainer);
            if (loader.size() == 0) {
                loader = $('<div id="' + settings.loaderContainer + '"><img class="img-loader" src="' + settings.loaderImg + '" /></div>');
                loader.hide();
            }
            //loader.css('left', settings.left);
            return loader;
        };

        // Inserts the loader and does a fadeIn.
        var loader = getLoader();
        var el = $(self).append(loader);
        loader.fadeIn();
    };

    // Removes the loader.
    $.fn.removeLoader = function (options) {
        var settings = $.extend({
            // These are the defaults, it will be completely overwritten 
            // by a property with the same key in the second or subsequent object
            loaderContainer: 'loader',
        }, options);
        var loader = $('#' + settings.loaderContainer);
        if (loader.size() > 0) {
            loader.remove();
        }
    };
} (jQuery));