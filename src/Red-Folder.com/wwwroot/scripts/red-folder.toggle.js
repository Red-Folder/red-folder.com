function toggle(group, item)
{
    var groupItems = $(group);

    var item = $(item);

    if (group != null && item != null)
    {
        if (!item.hasClass('hidden'))
        {
            item.addClass('hidden');
        }
        else
        {
            groupItems.addClass('hidden');
            item.removeClass('hidden');
        }
    }
}