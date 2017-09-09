/**
 * GoogleMap consumer
 */
(function ($) {
	$.fn.loadMap = function (options) {
		var self = this;
		var settings = $.extend({
			// These are the defaults, it will be completely overwritten 
			// by a property with the same key in the second or subsequent object
			address: '5, Phan Đình Phùng, Tân Thành, Tân Phú, HCM',
			markerInfo: '#map-marker',
			errorMsg: 'Không tìm thấy địa chỉ',
			markerIcon: '../Templates/Admin/js/plugins/jGoogleMap/marker.png',
			loaderIcon: '../Templates/Admin/js/plugins/jGoogleMap/loading.gif',
			tipboxIcon: '../Templates/Admin/js/plugins/jGoogleMap/tipbox.gif',
			closeIcon: '../Templates/Admin/js/plugins/jGoogleMap/close.gif',
			offsetY: -25
		}, options);
		
		// Return the active loader or creates a new loader
		var getLoader = function () {
			var loader = $(".map-loader");
			if (loader.size() == 0) {
				loader = $('<div class="map-loader"><img src="' + settings.loaderIcon + '" /></div>');
				loader.hide();
			}
			var w = '-' + ($(self).width()/2 - settings.offsetY) + 'px';
			loader.css('top', w);
			return loader;
		};
		
		// Inserts the loader and does a fadeIn.
		var showLoader = function() {
			var loader = getLoader();
			el = $(self).parent().after(loader);
			loader.fadeIn();
		};
		
		// Removes the loader.
		var removeLoader = function () {
			var loader = getLoader();
			loader.remove();
		};
		
		// Start show loader
		//showLoader();
		
		// Start to load map
		var mapOptions = {
			zoom: 16,
			mapTypeControl: true,
			mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
			navigationControl: true,
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};
		
		// $(this).get(0) => Retrieve the DOM elements matched by the jQuery object.
		var map = new google.maps.Map($(self).get(0), mapOptions);
		var geocoder = new google.maps.Geocoder();
		
		geocoder.geocode({ 'address': settings.address }, function(results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {
					map.setCenter(results[0].geometry.location);

					// Create marker
					var marker = new google.maps.Marker({
						position: results[0].geometry.location,
						map: map,
						icon: settings.markerIcon,
						title: settings.address
					});

					// Create InfoBox
					var boxText = document.createElement("div");
					boxText.style.cssText = "border: 1px solid black; margin-top: 8px; background: #000; padding: 5px;";
					boxText.innerHTML = $(settings.markerInfo).html();
		
					var mapInfoOptions = {
						content: boxText,
						disableAutoPan: false,
						maxWidth: 0,
						pixelOffset: new google.maps.Size(-140, 0),
						zIndex: null,
						boxStyle: { 
							background: 'url(' + settings.tipboxIcon + ') no-repeat',
							color: "#fff",
							opacity: 0.75,
							width: "280px"
						},
						closeBoxMargin: "10px 2px 2px 2px",
						closeBoxURL: settings.closeIcon,
						infoBoxClearance: new google.maps.Size(1, 1),
						isHidden: false,
						pane: "floatPane",
						enableEventPropagation: false
					};

					var infowindow = new InfoBox(mapInfoOptions);
					google.maps.event.addListener(marker, 'click', function() {
						infowindow.open(map, marker);
					});
				} else {
					app.logger.warning(settings.errorMsg);
				}
			} else {
				app.logger.warning(settings.errorMsg);
			}
			// Remove loader
			//removeLoader();
		});
	};
} (jQuery));
