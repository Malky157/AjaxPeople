$(() => {
    const addModal = new bootstrap.Modal($('#addModal')[0]);
    const editModal = new bootstrap.Modal($('#editModal')[0]);
    let id = 0;
    function refreshTable() {
        $("tbody").empty();
        $.get('/home/getpeople', function (people) {
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td><button class="btn btn-primary" id="edit" data-edit-id=${person.id}>Edit</button></td>
            <td><button class="btn btn-warning" id="delete" data-delete-id=${person.id}>Delete</button></td>
</tr>`)
            });
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show();
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = parseInt($("#age").val());
        $.post('/home/addperson', { firstName, lastName, age }, function () {
            addModal.hide();
            refreshTable();
        });
    });

    $('tbody').on('click', '#edit', function () {
        id = $(this).attr("data-edit-id");

        $.get('/home/editperson', { id }, function (person) {
            $("#editfirstName").val(person.firstName);
            $("#editlastName").val(person.lastName);
            $("#editage").val(person.age);
            editModal.show();
        })

    })
    $("#save-edit-person").on('click', function () {
        const firstName = $("#editfirstName").val();
        const lastName = $("#editlastName").val();
        const age = parseInt($("#editage").val());
        $.post('/home/editperson', { id, firstName, lastName, age }, function () {
            editModal.hide();
            id = 0;
            refreshTable();
        })


    })
    $('tbody').on('click', '#delete', function () {
        id = $(this).attr("data-delete-id");
        $.post('/home/deleteperson', { id }, function () {
            id = 0;
            refreshTable()
        })
    })
})