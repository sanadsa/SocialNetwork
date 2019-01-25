var notificationType = {
    Comment: 1,
    Like: 2,
    Follow: 3
};

function Notification(notificationSource, notificationDestination, text, type) {
    this.notificationSource = notificationSource;
    this.notificationDestination = notificationDestination;
    this.text = text;
    this.type = type;
}

function createNotification(notificationSource, notificationDestination, text, type) {
    var obj = {};
    obj.notificationSource = notificationSource;
    obj.notificationDestination = notificationDestination;
    obj.text = text;
    obj.type = type;
    return obj;
}
