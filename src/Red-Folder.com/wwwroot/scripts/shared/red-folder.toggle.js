function toggle(group, item) {
    'use strict';
    var $group = $(group);

    var $item = $(item);

    if ($group !== null && $item !== null) {
        if (!$item.hasClass('hidden')) {
            $item.addClass('hidden');
        } else {
            $group.addClass('hidden');
            $item.removeClass('hidden');
        }
    }
}
