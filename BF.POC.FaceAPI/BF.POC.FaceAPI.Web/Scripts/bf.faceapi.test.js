"use strict";

var bf = bf || {};
bf.faceapi = bf.faceapi || {};

bf.faceapi.components = {
    loadFileDrop: function (event) {
        bf.faceapi.test.loadFileDrop(event);
    }
};

bf.faceapi.test = {
    video: undefined,

    start: function () {
        bf.faceapi.core.setup();
        bf.faceapi.test.reset();

        $('#webcam-tab').on('click', bf.faceapi.test.initVideo);
        
        $('#ButtonURL').on('click', bf.faceapi.test.loadImage);
        $('#ButtonData').on('click', bf.faceapi.test.loadImage);
        $('#FieldLocal').on('change', bf.faceapi.test.loadFile);
        $('#ButtonWebcam').on('click', bf.faceapi.test.takeSnapshot);

        $('#Analyze').on('click', bf.faceapi.test.analyze);
    },

    reset: function (e) {
        $('#ImageDisplayer').addClass('d-none');
        $('#ImageSelector').removeClass('d-none');
    },

    initVideo: function () {
        bf.faceapi.core.initWebcam('VideoPlayer', function (video) {
            bf.faceapi.test.video = video;
        }, bf.faceapi.test.takeSnapshotHandleError);
    },

    loadImage: function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $field = $($(this).data('field'));

        if (!$field.val()) {
            return;
        }

        bf.faceapi.core.loadImage($field.val(), 'ImageContainer');
        bf.faceapi.test.setPhotoReady();
    },

    loadFile: function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($(this)[0].files.length != 1) {
            return;
        }

        var file = $(this)[0].files[0];

        if (!file.type.match('image.*')) {
            return;
        }

        bf.faceapi.core.loadImage(URL.createObjectURL(file), 'ImageContainer');
        bf.faceapi.test.setPhotoReady();
    },

    loadFileDrop: function (e) {
        if (e.dataTransfer && e.dataTransfer.files.length) {
            e.preventDefault();
            e.stopPropagation();

            var file = e.dataTransfer.files[0];

            if (!file.type.match('image.*')) {
                bf.faceapi.core.leaveDrag($('#DropLocal'));
                return;
            }

            bf.faceapi.core.loadImage(URL.createObjectURL(file), 'ImageContainer');
            bf.faceapi.test.setPhotoReady();
        }
    },

    takeSnapshot: function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('#MessageWebcam').addClass('d-none');

        try {
            bf.faceapi.core.takeSnapshot(bf.faceapi.test.video, 'ImageContainer', bf.faceapi.test.setPhotoReady, bf.faceapi.test.takeSnapshotHandleError);
        } catch (err) {
            bf.faceapi.test.takeSnapshotHandleError();
        }
    },

    takeSnapshotHandleError: function (message) {
        $('#MessageWebcam')
            .html(message || 'Something went wrong taking the snapshot.<br/>Please try again in a few minutes...')
            .removeClass('d-none');
    },

    setPhotoReady: function () {
        $('#ImageSelector').addClass('d-none');
        $('#ImageDisplayer').removeClass('d-none');
        $('#PanelAnalyze').removeClass('d-none');
    },

    analyze: function (e) {
        e.preventDefault();
        e.stopPropagation();

        var image = document.getElementById('ImageCanvas').toDataURL().slice(22);

        var data = new FormData();
        data.append('IMAGE', image);

        $('#PanelAnalyze').addClass('d-none');
        $('#Message').addClass('d-none');
        $("#InfoContainer").addClass('d-none');
        $('#Analyzing').removeClass('d-none');

        $.ajax({
            url: '/Test/Analyze',
            type: 'POST',
            data: data,
            async: true,
            cache: false,
            contentType: false,
            processData: false
        })
            .done(function (data) {
                $('#Analyzing').addClass('d-none');
                $('#PanelAnalyze').removeClass('d-none');

                bf.faceapi.test.showAnalisysInfo(data);
            })

            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#Analyzing').addClass('d-none');
                $('#PanelAnalyze').removeClass('d-none');

                $('#Message')
                    .html('Ups! Something went wrong.<br/>Please try again in a few minutes...')
                    .removeClass('d-none');
            });
    },

    showAnalisysInfo: function (result) {
        if (result.length > 0) {
            $("#InfoContainer").html(bf.faceapi.core.highlightSyntax(JSON.stringify(result, undefined, 4)));
            $("#InfoContainer").css("height", $("#ImageCanvas").height());
            $("#InfoContainer").removeClass('d-none');

            for (var i = 0; i < result.length; i++) {
                bf.faceapi.core.drawFaceRectangle(
                    result[i].FaceAttributes.Gender == 'female' ? '#FF0000' : (result[i].FaceAttributes.Gender == 'male' ? '#0000FF' : '#00FF00'),
                    result[i].FaceRectangle.Left,
                    result[i].FaceRectangle.Top,
                    result[i].FaceRectangle.Width,
                    result[i].FaceRectangle.Height
                );
            }
        } else {
            $('#Message')
                .html('No faces found in the selected image...')
                .removeClass('d-none');
        }
    }
}