/**=========================================================
 * Module: calendar-ui.js
 * This script handle the calendar demo with draggable 
 * events and events creations
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.extras')
        .directive('calendar', calendar);

    calendar.$inject = ['$rootScope', 'SocketService'];
    function calendar($rootScope, SocketService) {
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element) {

            if (!$.fn.fullCalendar) return;

            // The element that will display the calendar
            var calendar = element;
            if (localStorage.getItem('RSK_RM') == 'Y')
            {
                var url = "api/visitReport/GetRMCalenderDtl";
            }
            else if (localStorage.getItem('RSK_RM') == 'N') {
                var url = "api/zonalAllocation/GetZRMCalenderDtl";
            }
            else if (localStorage.getItem('CC') == 'Y') {
                var url = "api/MstCC/GetCCCalenderDtl";
            }
            else if (localStorage.getItem('AgrCC') == 'Y') {
                var url = "api/AgrTrnCC/GetCCCalenderDtl";
            }
            else {
                var url = "api/holidayCalender/event";
            }
          
            SocketService.get(url).then(function (resp) {

                var eventarray = resp.data.createevent;
                 
                if (eventarray == null) {

                    initExternalEvents(calendar);

                    initCalendar(calendar, demoEvents, $rootScope.app.layout.isRTL);
                }
                else
                {
                    var demoEvents = createDemoEvents(eventarray);

                    initExternalEvents(calendar);

                    initCalendar(calendar, demoEvents, $rootScope.app.layout.isRTL);
                }
                
            });
        }
    }


    // global shared var to know what we are dragging
    var draggingEvent = null;


    /**
     * ExternalEvent object
     * @param jQuery Object elements Set of element as jQuery objects
     */
    function ExternalEvent(elements) {

        if (!elements) return;

        elements.each(function () {
            var $this = $(this);
            // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
            // it doesn't need to have a start or end
            var calendarEventObject = {
                title: $.trim($this.text()) // use the element's text as the event title
            };

            // store the Event Object in the DOM element so we can get to it later
            $this.data('calendarEventObject', calendarEventObject);

            // make the event draggable using jQuery UI
            $this.draggable({
                zIndex: 1070,
                revert: true, // will cause the event to go back to its
                revertDuration: 0  //  original position after the drag
            });

        });
    }

    /**
     * Invoke full calendar plugin and attach behavior
     * @param  jQuery [calElement] The calendar dom element wrapped into jQuery
     * @param  EventObject [events] An object with the event list to load when the calendar displays
     */
    function initCalendar(calElement, events, isRTL) {

        // check to remove elements from the list
        var removeAfterDrop = $('#remove-after-drop');

        calElement.fullCalendar({
            isRTL: isRTL,
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            buttonIcons: { // note the space at the beginning
                prev: ' fa fa-caret-left',
                next: ' fa fa-caret-right'
            },
            buttonText: {
                today: 'today',
                month: 'month',
                week: 'week',
                day: 'day'
            },
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar 
            drop: function (date, allDay) { // this function is called when something is dropped

                var $this = $(this),
                    // retrieve the dropped element's stored Event Object
                    originalEventObject = $this.data('calendarEventObject');

                // if something went wrong, abort
                if (!originalEventObject) return;

                // clone the object to avoid multiple events with reference to the same object
                var clonedEventObject = $.extend({}, originalEventObject);

                // assign the reported date
                clonedEventObject.start = date;
                clonedEventObject.allDay = allDay;
                clonedEventObject.backgroundColor = $this.css('background-color');
                clonedEventObject.borderColor = $this.css('border-color');

                // render the event on the calendar
                // the last `true` argument determines if the event "sticks" 
                // (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                calElement.fullCalendar('renderEvent', clonedEventObject, true);

                // if necessary remove the element from the list
                if (removeAfterDrop.is(':checked')) {
                    $this.remove();
                }
            },
            eventDragStart: function (event/*, js, ui*/) {
                draggingEvent = event;
            },
            // This array is the events sources
            events: events
        });
    }

    /**
     * Inits the external events panel
     * @param  jQuery [calElement] The calendar dom element wrapped into jQuery
     */
    function initExternalEvents(calElement) {
        // Panel with the external events list
        var externalEvents = $('.external-events');

        // init the external events in the panel
        new ExternalEvent(externalEvents.children('div'));

        // External event color is danger-red by default
        var currColor = '#f6504d';
        // Color selector button
        var eventAddBtn = $('.external-event-add-btn');
        // New external event name input
        var eventNameInput = $('.external-event-name');
        // Color switchers
        var eventColorSelector = $('.external-event-color-selector .circle');

        // Trash events Droparea 
        $('.external-events-trash').droppable({
            accept: '.fc-event',
            activeClass: 'active',
            hoverClass: 'hovered',
            tolerance: 'touch',
            drop: function (event, ui) {

                // You can use this function to send an ajax request
                // to remove the event from the repository

                if (draggingEvent) {
                    var eid = draggingEvent.id || draggingEvent._id;
                    // Remove the event
                    calElement.fullCalendar('removeEvents', eid);
                    // Remove the dom element
                    ui.draggable.remove();
                    // clear
                    draggingEvent = null;
                }
            }
        });

        eventColorSelector.click(function (e) {
            e.preventDefault();
            var $this = $(this);

            // Save color
            currColor = $this.css('background-color');
            // De-select all and select the current one
            eventColorSelector.removeClass('selected');
            $this.addClass('selected');
        });

        eventAddBtn.click(function (e) {
            e.preventDefault();

            // Get event name from input
            var val = eventNameInput.val();
            // Dont allow empty values
            if ($.trim(val) === '') return;

            // Create new event element
            var newEvent = $('<div/>').css({
                'background-color': currColor,
                'border-color': currColor,
                'color': '#fff'
            })
                            .html(val);

            // Prepends to the external events list
            externalEvents.prepend(newEvent);
            // Initialize the new event element
            new ExternalEvent(newEvent);
            // Clear input
            eventNameInput.val('');
        });
    }

    /**
     * Creates an array of events to display in the first load of the calendar
     * Wrap into this function a request to a source to get via ajax the stored events
     * @return Array The array with the events
     */
    function createDemoEvents(eventarray) {
        var i = 0, totalcount = eventarray.length;
        var items = [];
        for (i = 0; i < totalcount; i++) {
            var eventarraylist = eventarray[i];
            
            let date = new Date(eventarraylist.event_date);
            
            var d = date.getDate(),
                m = date.getMonth(),
                y = date.getFullYear();
           
            let time =new Date(eventarraylist.event_time)
            var HH = time.getHours(),
                mm = time.getMinutes();
            
            eventarraylist = {
              
                title: eventarraylist.event_title,
                time : time,
                start: new Date(y, m, d, HH, mm,'0','0'),
                             backgroundColor: '#f56954', //red 
                             borderColor: '#f56954' //red
                
            }
          
            items.push(eventarraylist);
           
        }
        return items;
    }

})();
