"use strict";

var bf = bf || {};
bf.faceapi = bf.faceapi || {};

bf.faceapi.components = {
    loadFileDrop: function (event) {
        bf.faceapi.groups.loadFileDrop(event);
    }
};

bf.faceapi.groups = {
    video: undefined,

    startSearch: function () {
        bf.faceapi.core.setup();
        bf.faceapi.groups.resetSearch();

        $('#webcam-tab').on('click', bf.faceapi.groups.initVideo);

        $('#ButtonURL').on('click', bf.faceapi.groups.loadImage);
        $('#ButtonData').on('click', bf.faceapi.groups.loadImage);
        $('#FieldLocal').on('change', bf.faceapi.groups.loadFile);
        $('#ButtonWebcam').on('click', bf.faceapi.groups.takeSnapshot);

        $('#Search').on('click', bf.faceapi.groups.search);
        $('#Restart').on('click', bf.faceapi.groups.restart);
    },

    resetSearch: function (e) {
        $('#ImageDisplayer').addClass('d-none');
        $('#ImageSelector').removeClass('d-none');
    },

    initVideo: function () {
        bf.faceapi.core.initWebcam('VideoPlayer', function (video) {
            bf.faceapi.groups.video = video;
        }, bf.faceapi.groups.takeSnapshotHandleError);
    },

    loadImage: function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $field = $($(this).data('field'));

        if (!$field.val()) {
            return;
        }

        bf.faceapi.core.loadImage($field.val(), 'ImageContainer');
        bf.faceapi.groups.setPhotoReady();
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
        bf.faceapi.groups.setPhotoReady();
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
            bf.faceapi.groups.setPhotoReady();
        }
    },

    takeSnapshot: function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('#MessageWebcam').addClass('d-none');

        try {
            bf.faceapi.core.takeSnapshot(bf.faceapi.groups.video, 'ImageContainer', bf.faceapi.groups.setPhotoReady, bf.faceapi.groups.takeSnapshotHandleError);
        } catch (err) {
            bf.faceapi.groups.takeSnapshotHandleError();
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
        $('#PanelSearch').removeClass('d-none');
    },

    restart: function (e) {
        $('#ImageDisplayer').addClass('d-none');
        $('#PanelSearch').addClass('d-none');
        $('#ImageSelector').removeClass('d-none');
        $("#InfoContainer").addClass('d-none');

        $('#ImageContainer').html('');
        $("#InfoContainer table tbody").html('');
    },

    search: function (e) {
        e.preventDefault();
        e.stopPropagation();

        $("#InfoContainer table tbody").html('');

        var IMAGE = document.getElementById('ImageCanvas').toDataURL().slice(22);

        var data = new FormData();
        data.append('IMAGE', IMAGE);

        $('#PanelSearch').addClass('d-none');
        $('#Message').addClass('d-none');
        $("#InfoContainer").addClass('d-none');
        $('#Searching').removeClass('d-none');

        $.ajax({
            url: '/Groups/DoSearch/' + window.location.pathname.replace('/Groups/Search/', ''),
            type: 'POST',
            data: data,
            async: true,
            cache: false,
            contentType: false,
            processData: false
        })
            .done(function (data) {
                $('#Searching').addClass('d-none');
                $('#PanelSearch').removeClass('d-none');

                bf.faceapi.groups.showAnalisysInfo(data);
            })

            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#Searching').addClass('d-none');
                $('#PanelSearch').removeClass('d-none');

                $('#Message')
                    .html('Ups! Something went wrong.<br/>Please try again in a few minutes...')
                    .removeClass('d-none');
            });
    },

    showAnalisysInfo: function (result) {
        if (result.length > 0) {
            $("#InfoContainer").removeClass('d-none');

            var tbody = $("#InfoContainer table tbody")[0];

            for (var i = 0; i < result.length; i++) {
                var color = result[i].Gender == 'female' ? '#FF0000' : (result[i].Gender == 'male' ? '#0000FF' : '#00FF00');

                bf.faceapi.core.drawFaceRectangle(
                    color,
                    result[i].FaceRectangle.Left,
                    result[i].FaceRectangle.Top,
                    result[i].FaceRectangle.Width,
                    result[i].FaceRectangle.Height
                );

                if (result[i].Confidence > 0) {
                    bf.faceapi.core.addFaceName(
                        result[i].Fullname,
                        color,
                        result[i].FaceRectangle.Left + (result[i].FaceRectangle.Width / 2),
                        result[i].FaceRectangle.Top + result[i].FaceRectangle.Height + 5
                   );
                }

                var row = tbody.insertRow(tbody.rows.length);
                var cellId = row.insertCell(0);
                var cellFullname = row.insertCell(1);
                var cellConfidence = row.insertCell(2);

                cellId.innerHTML = result[i].Id;
                cellFullname.innerHTML = result[i].Fullname.trim() || '-Unknown-';
                cellConfidence.innerHTML = (result[i].Confidence * 100).toFixed(2) + '%'
            }
        } else {
            $('#Message')
                .html('No matches found in current group...')
                .removeClass('d-none');
        }
    }
}