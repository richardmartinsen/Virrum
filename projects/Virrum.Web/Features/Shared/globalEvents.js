define([
    'deco/proclaimWhen'
], function (
    proclaimWhen
) {
    return proclaimWhen.extend({
        selectedTrackHasChanged: function (trackId) { },
        selectedSchoolHasChanged: function (schoolId) {},
        selectedBookingHasChanged: function (bookingId) { },
        selectedGroupHasChanged: function (groupId) { },
        practiceGroupsHasChanged: function () { },
        selectedPeriodHasChanged: function () { },
        selectedUserHasChanged: function (userId) { },
        aDrivingSchoolHasChanged: function (school) { }
    });
});