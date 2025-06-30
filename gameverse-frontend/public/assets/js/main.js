(function ($) {
	"use strict";

	// Mobile Nav toggle
	if ($('.menu-toggle > a').length) {
		$('.menu-toggle > a').on('click', function (e) {
			e.preventDefault();
			$('#responsive-nav').toggleClass('active');
		});
	}

	// Fix cart dropdown from closing
	if ($('.cart-dropdown').length) {
		$('.cart-dropdown').on('click', function (e) {
			e.stopPropagation();
		});
	}

	// Products Slick
	if ($('.products-slick').length && $.fn.slick) {
		$('.products-slick').each(function () {
			var $this = $(this),
				$nav = $this.attr('data-nav');

			$this.slick({
				slidesToShow: 4,
				slidesToScroll: 1,
				autoplay: true,
				infinite: true,
				speed: 300,
				dots: false,
				arrows: true,
				appendArrows: $nav ? $nav : false,
				responsive: [
					{ breakpoint: 991, settings: { slidesToShow: 2, slidesToScroll: 1 } },
					{ breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1 } },
				],
			});
		});
	}

	// Products Widget Slick
	if ($('.products-widget-slick').length && $.fn.slick) {
		$('.products-widget-slick').each(function () {
			var $this = $(this),
				$nav = $this.attr('data-nav');

			$this.slick({
				infinite: true,
				autoplay: true,
				speed: 300,
				dots: false,
				arrows: true,
				appendArrows: $nav ? $nav : false,
			});
		});
	}

	// Product image slider
	if ($('#product-main-img').length && $('#product-imgs').length && $.fn.slick) {
		$('#product-main-img').slick({
			infinite: true,
			speed: 300,
			dots: false,
			arrows: true,
			fade: true,
			asNavFor: '#product-imgs',
		});

		$('#product-imgs').slick({
			slidesToShow: 3,
			slidesToScroll: 1,
			arrows: true,
			centerMode: true,
			focusOnSelect: true,
			centerPadding: 0,
			vertical: true,
			asNavFor: '#product-main-img',
			responsive: [{
				breakpoint: 991,
				settings: {
					vertical: false,
					arrows: false,
					dots: true,
				},
			}],
		});
	}

	// Product zoom
	if ($('#product-main-img').length && $.fn.zoom) {
		$('#product-main-img .product-preview').zoom();
	}

	// Input number with +/- buttons
	if ($('.input-number').length) {
		$('.input-number').each(function () {
			var $this = $(this),
				$input = $this.find('input[type="number"]'),
				up = $this.find('.qty-up'),
				down = $this.find('.qty-down');

			down.on('click', function () {
				var value = parseInt($input.val()) - 1;
				value = value < 1 ? 1 : value;
				$input.val(value);
				$input.change();
				updatePriceSlider($this, value);
			});

			up.on('click', function () {
				var value = parseInt($input.val()) + 1;
				$input.val(value);
				$input.change();
				updatePriceSlider($this, value);
			});
		});
	}

	// Price slider + inputs
	var priceInputMax = document.getElementById('price-max');
	var priceInputMin = document.getElementById('price-min');
	var priceSlider = document.getElementById('price-slider');

	if (priceSlider && typeof noUiSlider !== 'undefined') {
		noUiSlider.create(priceSlider, {
			start: [1, 999],
			connect: true,
			step: 1,
			range: {
				'min': 1,
				'max': 999,
			}
		});

		if (priceInputMax && priceInputMin) {
			priceSlider.noUiSlider.on('update', function (values, handle) {
				var value = values[handle];
				if (handle) priceInputMax.value = value;
				else priceInputMin.value = value;
			});

			priceInputMax.addEventListener('change', function () {
				updatePriceSlider($(this).parent(), this.value);
			});
			priceInputMin.addEventListener('change', function () {
				updatePriceSlider($(this).parent(), this.value);
			});
		}
	}

	// Slider update function
	function updatePriceSlider(elem, value) {
		if (!priceSlider || typeof priceSlider.noUiSlider === 'undefined') return;

		if (elem.hasClass('price-min')) {
			priceSlider.noUiSlider.set([value, null]);
		} else if (elem.hasClass('price-max')) {
			priceSlider.noUiSlider.set([null, value]);
		}
	}

})(jQuery);
