window.backTop = function () {
    slideTo(0);
}

function slideTo(targetPageY) {
    var timer = setInterval(function () {
        var currentY = document.documentElement.scrollTop || document.body.scrollTop;
        var distance = targetPageY > currentY ? targetPageY - currentY : currentY - targetPageY;
        var speed = Math.ceil(distance / 10);
        if (currentY == targetPageY || currentY == 1) {
            document.documentElement.scrollTop = 0;
            clearInterval(timer);
        } else {
            window.scrollTo(0, targetPageY > currentY ? currentY + speed : currentY - speed);
        }
    }, 10);
}