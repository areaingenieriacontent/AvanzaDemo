$(document).ready(function () {
    $('#calendar').fullCalendar({
        locale: 'es',
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '../Calendario/GetCalendarData',
                type: "GET",
                dataType: "JSON",
                success: function (result) {
                    var events = [];
                    $.each(result, function (i, data) {
                        events.push(
                            {
                                id: data.Id,
                                title: data.Title,
                                description: data.Description,
                                start: data.StartDate,
                                end: data.EndDate,
                                backgroundColor: data.BackgroundColor,
                                borderColor: data.BorderColor,
                                url: data.EventRoute
                            });
                    });
                    callback(events);
                }
            });
        },
        eventClick: function (info) {
            info.jsEvent.preventDefault();

            if (info.event.url) {
                window.open(info.event.url);
            }
        },
        eventRender: function (event, element) {
            element.qtip(
                {
                    content: event.description
                });
        },
        editable: false
    });
});