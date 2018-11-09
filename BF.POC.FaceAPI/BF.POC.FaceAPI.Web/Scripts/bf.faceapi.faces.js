"use strict";

var bf = bf || {};
bf.faceapi = bf.faceapi || {};

bf.faceapi.components = {
    loadFileDrop: function (event) {
        bf.faceapi.faces.loadFileDrop(event);
    }
};

bf.faceapi.faces = {
    video: undefined,

    startList: function () {
        bf.faceapi.core.setup();

        $('#FaceList tr').on('click', bf.faceapi.faces.showFace);
    },

    startAdd: function () {
        bf.faceapi.core.setup();
        bf.faceapi.faces.resetAdd();

        $('#webcam-tab').on('click', bf.faceapi.faces.initVideo);

        $('#ButtonURL').on('click', bf.faceapi.faces.loadImage);
        $('#ButtonData').on('click', bf.faceapi.faces.loadImage);
        $('#FieldLocal').on('change', bf.faceapi.faces.loadFile);
        $('#ButtonWebcam').on('click', bf.faceapi.faces.takeSnapshot);

        $('#Change').on('click', bf.faceapi.faces.changeFace);
        $('#Add').on('click', bf.faceapi.faces.addFace);
    },

    resetAdd: function (e) {
        $('#ImageDisplayer').addClass('d-none');
        $('#ImageSelector').removeClass('d-none');

        bf.faceapi.faces.disableButtons();
    },

    showFace: function (e) {
        var src = $(this).find('img').attr('src');

        var image = new Image();
        image.crossOrigin = "anonymous";
        image.className = 'mw-100';
        image.src = src;

        $('#ImageModalTitle').html($(this).find('td.fullname').html());

        $('#ImageModalContainer').html('');
        document.getElementById('ImageModalContainer').appendChild(image);
    },

    initVideo: function () {
        bf.faceapi.core.initWebcam('VideoPlayer', function (video) {
            bf.faceapi.faces.video = video;
        }, bf.faceapi.faces.takeSnapshotHandleError);
    },

    loadImage: function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $field = $($(this).data('field'));

        if (!$field.val()) {
            return;
        }

        bf.faceapi.core.loadImage($field.val(), 'ImageContainer');
        bf.faceapi.faces.setPhotoReady();
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
        bf.faceapi.faces.setPhotoReady();
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
            bf.faceapi.faces.setPhotoReady();
        }
    },

    takeSnapshot: function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('#MessageWebcam').addClass('d-none');

        try {
            bf.faceapi.core.takeSnapshot(bf.faceapi.faces.video, 'ImageContainer', bf.faceapi.faces.setPhotoReady, bf.faceapi.faces.takeSnapshotHandleError);
        } catch (err) {
            bf.faceapi.faces.takeSnapshotHandleError();
        }
    },

    takeSnapshotHandleError: function (message) {
        $('#MessageWebcam')
            .html(message || 'Something went wrong taking the snapshot.<br/>Please try again in a few minutes...')
            .removeClass('d-none');
    },

    setPhotoReady: function () {
        $('#ImageSelector').find('input,textarea').val('');
        $('#ImageSelectorTab li:first-child a').tab('show');

        $('#ImageSelector').addClass('d-none');
        $('#ImageDisplayer').removeClass('d-none');

        $('span[data-valmsg-for="Photo"]').html('');

        bf.faceapi.faces.enableButtons();
    },

    changeFace: function (e) {
        $('#ImageDisplayer').addClass('d-none');
        $('#ImageSelector').removeClass('d-none');

        $('#ImageContainer').html('');

        bf.faceapi.faces.disableButtons();
    },

    addFace: function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('#div.validation-summary-errors ul').html('');

        if (!document.getElementById('ImageCanvas')) {
            $('span[data-valmsg-for="Photo"]').html("The field 'Photo' is mandatory");
            return;
        } else {
            $('span[data-valmsg-for="Photo"]').html('');
        }

        var image = document.getElementById('ImageCanvas').toDataURL().slice(22);

        var data = new FormData();
        data.append('PersonId', $('#PersonId').val());
        data.append('Photo', image);

        bf.faceapi.faces.disableButtons();

        $.ajax({
            url: '/Faces/Add',
            type: 'POST',
            data: data,
            async: true,
            cache: false,
            contentType: false,
            processData: false
        })
            .done(function (data) {
                window.location.href = $('#Cancel').attr('href')
            })

            .fail(function (jqXHR, textStatus, errorThrown) {
                bf.faceapi.faces.enableButtons();

                $('div.validation-summary-errors ul').html('<li>' + errorThrown + '</li>');
            });
    },

    enableButtons: function () {
        $('#Change').removeClass('disabled').attr('disabled', null);
        $('#Add').removeClass('disabled').attr('disabled', null);
        $('#Cancel').removeClass('disabled').attr('disabled', null);
    },

    disableButtons: function () {
        $('#Change').addClass('disabled').attr('disabled', 'disabled');
        $('#Add').addClass('disabled').attr('disabled', 'disabled');
        $('#Cancel').addClass('disabled').attr('disabled', 'disabled');
    }

}

