function toggle(group, item) {
    'use strict';
    if (group != null) {
        var $group = $(group);

        if ($group != null) {
            $item.each(function (index) {
                if (!$(this).hasClass('hidden')) {
                    $(this).addClass('hidden');
                }
            });
        }
    }

    if (item != null) {
        var $item = $(item);

        if ($item != null) {
            $item.each(function (index) {
                if ($(this).hasClass('hidden')) {
                    $(this).removeClass('hidden');
                } else {
                    $(this).addClass('hidden');
                }
            });
        }
    }
}
