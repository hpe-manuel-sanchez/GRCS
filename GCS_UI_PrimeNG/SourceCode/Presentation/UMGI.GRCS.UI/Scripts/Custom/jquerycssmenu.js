//Specify full URL to down and right arrow images (25 is padding-right to add to top level LIs with drop downs):
var arrowimages = {
    down: ['downarrowclass', '/GCS/Images/arrow-down.png', 25],
    right: ['rightarrowclass', '/GCS/Images/Arrow-right-new_trn.png', 5]
}

var jquerycssmenu = {
    fadesettings: { overduration: 350, outduration: 100 }, //duration of fade in/ out animation, in milliseconds

    buildmenu: function (menuid, arrowsvar) {
        jQuery(document).ready(function ($) {
            var $mainmenu = $("#" + menuid + ">ul");
            var $headers = $mainmenu.find("ul").parent();

            $headers.each(function (i) {
                var $curobj = $(this);
                var $subul = $(this).find('ul:eq(0)');
                this._dimensions = { w: this.offsetWidth, h: this.offsetHeight, subulw: $subul.outerWidth(), subulh: $subul.outerHeight() };
                this.istopheader = $curobj.parents("ul").length == 1 ? true : false;

                var defaultTop = 0;

                $subul.css({ marginTop: this.istopheader ? this._dimensions.h + "px" : defaultTop });
                $subul.css({ top: 0 });
                var $innerheader = $curobj.children('a').eq(0);

                $innerheader = ($innerheader.children().eq(0).is('span')) ? $innerheader.children().eq(0) : $innerheader //if header contains inner SPAN, use that

                $innerheader.append(
                    '<img src="' + (this.istopheader ? arrowsvar.down[1] : arrowsvar.right[1])
                    + '" class="' + (this.istopheader ? arrowsvar.down[0] : arrowsvar.right[0])
                    + '" style="border:0;" />');

                $curobj.hover(
                    function (e) {
                        var $targetul = $(this).children("ul:eq(0)")
                        if ($curobj.hasClass('last')) {
                            $curobj.removeClass('last');
                            $curobj.addClass('lastHover');
                        }
                        $innerheader.parent().css('background', '#485472')
                        $innerheader.parent().css('color', '#fff')
                        this._offsets = {
                            left: $(this).offset().left,
                            top: $(this).offset().top
                        }
                        var menuleft = this.istopheader ? 0 : this._dimensions.w
                        menuleft = (this._offsets.left + menuleft + this._dimensions.subulw > $(window).width())
                            ? (this.istopheader
                                ? -this._dimensions.subulw + this._dimensions.w
                                : -this._dimensions.w)
                            : menuleft
                        $targetul.css({ left: menuleft + "px" }).fadeIn(jquerycssmenu.fadesettings.overduration);

                        /* Added for visibility in IE7 Problem*/
                        $targetul.css({ visibility: 'visible' });
                        $('ul', $targetul).css({ display: 'none', visibility: 'hidden' });
                    },
                    function (e) {
                        $(this).children("ul:eq(0)").fadeOut(jquerycssmenu.fadesettings.outduration)
                        if ($curobj.hasClass('lastHover')) {
                            $curobj.removeClass('lastHover');
                            $curobj.addClass('last');
                        }
                        $innerheader.parent().css('background', '#7ea121')
                    });
                $(".menu_level_3").css({ display: 'none', visibility: 'hidden' });
            }); 

            $mainmenu.find("ul").css({
                display: 'none',
                visibility: 'visible'
            });
        })
    }
}

jquerycssmenu.buildmenu("myjquerymenu", arrowimages)