$(function() {
    $('#primerSelect').change(function() {
        var id = $(this).val();
        if (id) {
            var segundoSelect = $('#segundoSelect');
            $('#segundoSelect').empty().append('<option>Loading...</option>');
            $.ajax({
                url: getValoresSegundoSelectUrl,
                data: { id: id },
                success: function (data) {
                    segundoSelect.empty();
                    $.each(data, function (index, value) {
                        segundoSelect.append($('<option value="' + value.value + '">' + value.text + '</option>'));
                    });
                }
            });
            $("#div-fechas").show();
        }   
    });
    $('#segundoSelect').change(function () {
        var selectedOption1 = $('#primerSelect').val();
        var matchday = $(this).val();
        if (selectedOption1 && matchday) {
            $('#loadingMessage').show();
            $.ajax({
                url: getCodeLeagueUrl,
                data: { id: selectedOption1 },
                success: function (data) {
                    var code = data;
                    window.location.href = 'Home/Fixture?code=' + code + '&matchday=' + matchday;

                }
            });
        }
    });
});
    