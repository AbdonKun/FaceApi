"use strict";

var bf = bf || {};
bf.faceapi = bf.faceapi || {};

bf.faceapi.core = {
    setup: function () {
        $('div.drop-zone').on({
            'dragenter dragover': bf.faceapi.core.dragTurnOn,
            'dragleave': bf.faceapi.core.dragTurnOff
        });
    },

    initWebcam: function (container, succes, error) {
        var video = { player: undefined, stream: undefined };

        // Older browsers might not implement mediaDevices at all, so we set an empty object first
        if (navigator.mediaDevices === undefined) {
            navigator.mediaDevices = {};
        }

        // Some browsers partially implement mediaDevices. We can't just assign an object with getUserMedia as it would overwrite existing properties.
        // Here, we will just add the getUserMedia property if it's missing.
        if (navigator.mediaDevices.getUserMedia === undefined) {
            navigator.mediaDevices.getUserMedia = function (constraints) {

                // First get ahold of the legacy getUserMedia, if present
                var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;

                // Some browsers just don't implement it - return a rejected promise with an error to keep a consistent interface
                if (!getUserMedia) {
                    return Promise.reject(new Error('getUserMedia is not implemented in current browser'));
                }

                // Otherwise, wrap the call to the old navigator.getUserMedia with a Promise
                return new Promise(function (resolve, reject) {
                    getUserMedia.call(navigator, constraints, resolve, reject);
                });
            }
        }

        navigator.mediaDevices.getUserMedia({ audio: false, video: true })
            .then(function (stream) {
                video.player = document.getElementById(container);

                // Older browsers may not have srcObject
                if ('srcObject' in video.player) {
                    video.player.srcObject = stream;
                } else {
                    // Avoid using this in new browsers, as it is going away.
                    video.player.src = window.URL.createObjectURL(stream);
                }
                video.stream = stream;

                succes(video);
            })
            .catch(function (err) {
                console.log('The following error occured: ' + err.name + ' - ' + err.message);
                error();
            });
    },

    takeSnapshot: function (video, imageContainer, success, error) {
        if (typeof InstallTrigger !== 'undefined' || !!window.StyleMedia) { //isFirefox || isEdge
            bf.faceapi.core.renderImage(video.player, video.player.videoWidth, video.player.videoHeight);

            video.stream.getVideoTracks()[0].stop();

            success();
        } else {
            var imageCapture = new ImageCapture(video.stream.getVideoTracks()[0]);

            imageCapture.grabFrame()
                .then(image => {
                    video.stream.getVideoTracks()[0].stop();

                    bf.faceapi.core.renderImage(image, image.width, image.height);

                    success();
                })
                .catch(function (err) {
                    console.log('The following error occured: ' + err.name + ' - ' + err.message);
                    error();
                });
        }
    },

    loadImage: function (src, container) {
        var image = new Image();
        image.crossOrigin = "anonymous";
        image.onload = function () {
            bf.faceapi.core.renderImage(image, image.width, image.height, container);
        };
        image.src = src;
    },

    renderImage: function (image, width, height, container) {
        var imageCanvas = document.createElement('canvas');
        imageCanvas.setAttribute('id', 'ImageCanvas');
        imageCanvas.width = width;
        imageCanvas.height = height;
        imageCanvas.className = 'mw-100';
        document.getElementById(container).appendChild(imageCanvas);

        var imageCanvasContext = imageCanvas.getContext('2d');
        imageCanvasContext.drawImage(image, 0, 0);
    },

    drawFaceRectangle: function (color, left, top, width, height) {
        var imageCanvas = document.getElementById('ImageCanvas');
        var imageCanvasContext = imageCanvas.getContext('2d');

        imageCanvasContext.beginPath();
        imageCanvasContext.lineWidth = '4';
        imageCanvasContext.strokeStyle = color;
        imageCanvasContext.rect(left - 2, top - 2, width + 4, height + 4);
        imageCanvasContext.stroke();
    },

    addFaceName: function (name, color, left, top) {
        var imageCanvas = document.getElementById('ImageCanvas');
        var imageCanvasContext = imageCanvas.getContext('2d');

        imageCanvasContext.font = '30px Arial';
        imageCanvasContext.fillStyle = color;
        imageCanvasContext.textAlign = 'center';
        imageCanvasContext.textBaseline = "top";
        imageCanvasContext.fillText(name, left, top);
    },

    highlightSyntax: function (json) {
        json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
        return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
            var cls = 'number';
            if (/^"/.test(match)) {
                if (/:$/.test(match)) {
                    cls = 'key';
                } else {
                    cls = 'string';
                }
            } else if (/true|false/.test(match)) {
                cls = 'boolean';
            } else if (/null/.test(match)) {
                cls = 'null';
            }
            return '<span class="' + cls + '">' + match + '</span>';
        });
    },

    dragTurnOn: function (e) {
        e.preventDefault();
        e.stopPropagation();

        bf.faceapi.core.enterDrag($(this));
    },

    dragTurnOff: function (e) {
        e.preventDefault();
        e.stopPropagation();

        bf.faceapi.core.leaveDrag($(this));
    },

    enterDrag: function ($element) {
        $element.css('border', '1px dashed red');
        $element.css('background-color', '#F5F5F5');
    },

    leaveDrag: function ($element) {
        $element.css('border', '1px dashed black');
        $element.css('background-color', '#FFFFFF');
    }

}