// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function html() {
    $("h2").html("HTML");
    $("p.p1").html("HTML (stands for Hypertext Markup Language) is a computer language that makes up most web pages and online applications. A hypertext is a text that is used to reference other pieces of text, while a markup language is a series of markings that tells web servers the style and structure of a document.");
    $("img").attr("src", "https://www.gamelab.id/uploads/news/berita-213-belajar-mengenal-dasar-html-20200716-163110.png")
}
function css() {
    $("h2").html("CSS");
    $("p.p1").html("CSS is designed to enable the separation of presentation and content, including layout, colors, and fonts.[3] This separation can improve content accessibility; provide more flexibility and control in the specification of presentation characteristics; enable multiple web pages to share formatting by specifying the relevant CSS in a separate .css file, which reduces complexity and repetition in the structural content; and enable the .css file to be cached to improve the page load speed between the pages that share the file and its formatting.");
    $("img").attr("src", "https://codelatte.org/wp-content/uploads/2018/11/CSS.jpg")
}
function js() {
    $("h2").html("JavaScript");
    $("p.p1").html("JavaScript is a scripting or programming language that allows you to implement complex features on web pages — every time a web page does more than just sit there and display static information for you to look at — displaying timely content updates, interactive maps, animated 2D/3D graphics, scrolling video jukeboxes, etc. — you can bet that JavaScript is probably involved. It is the third layer of the layer cake of standard web technologies, two of which (HTML and CSS) we have covered in much more detail in other parts of the Learning Area.");
    $("img").attr("src", "https://javascriptoo.com/wp-content/uploads/2021/07/Mengenal-Tentang-Pemrograman-JavaScript-Untuk-Pemula.png");
}
function ubah(){
    $(" .col#kolom1").css({ "background-color": "yellow", "font-size": "200%" });
}

const animals = [
    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
]
let onlyCat = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        onlyCat.push(animals[i]);
    }
    else {
        animals[i].kelas.name = 'non-mamalia';
    }
}
console.log("Only Cat");
for (let i = 0; i < onlyCat.length; i++) {
    console.log(onlyCat[i]);
}
console.log("New Animals");
for (let i = 0; i < animals.length; i++) {
    console.log(animals[i]);
}

///Pokemon
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result.results);
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                <td>${key+1}</td>
                <td>${val.name}</td>
                <td><button class="btn btn-primary" onclick="getDetail('${val.url}')" data-toggle="modal" data-target="#detailModal">Detail</button></td>
        </tr>`;
    });
    console.log(text);
    $(".tablePoke").html(text);
}).fail((error) => {
    console.log(error);
});

//Pokemon Get Detail
function getDetail(link) {
    $.ajax({
        url: link
    }).done((result) => {
        console.log(result);
        var ability = "";
        $.each(result.abilities, function (key, val) {
            ability += `<span class="ability" style="text-align">${val.ability.name}</span> &nbsp`;
        });
        var typePoke = "";
        $.each(result.types, function (key, val) {
            typePoke += typePokeColor(val.type.name) + "&nbsp";
        });
        function typePokeColor(val) {
            if (val == "grass") {
                var color = `<span class="badge badge-success badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else if (val == "water") {
                var color = `<span class="badge badge-primary badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else if (val == "poison") {
                var color = `<span class="badge badge-dark badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else if (val == "normal") {
                var color = `<span class="badge badge-light badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else if (val == "fire") {
                var color = `<span class="badge badge-danger badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else if (val == "electric") {
                var color = `<span class="badge badge-warning badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
            else {
                var color = `<span class="badge badge-secondary badge-pill" style="text-align: center;">${val}</span>`;
                return color;
            }
        }
        var statPoke = "";
        $.each(result.stats, function (key, val) {
            statPoke += `<div class="row">
                         <div class="col" id="base" style="text-transform:uppercase; font-size: 13px"><b>${val.stat.name}</b></div>
                         <div class="baseStat col">: ${val.base_stat}</div></div>`;
        });
        var img = ""
        img += `
            <img src="${result.sprites.other.dream_world.front_default}" alt="" width="200" height="200" style="background-color:lightgrey;" class="rounded-circle shadow p-0 mb-1 rounded"/></div>`;
        $(".detailName").html(result.name);
        $(".ability").html(": " + ability);
        $(".height").html(": " + result.height + " m");
        $(".weight").html(": " + result.weight + " Lbs");
        $("#stat").html(statPoke);
        $(".badge").html(typePoke);
        $("#detailImage").html(img);
    }).fail((error) => {
        console.log(error);
    });
}
chartGender();
chartUniversity();
chartSalary();

/*================================Show Data Table================================*/
var table = "";
$(document).ready(function () {
    table = $('#tableEmployee').DataTable({
        'dom': 'Bfrtip',
        'buttons': [
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"> </i>',
                titleAttr: 'Copy'
            },
            {
                extend: 'excelHtml5' ,
                text: '<i class="fa fa-file-excel-o"> </i>',
                titleAttr: 'Excel',
                exportOptions: {
                    columns:[1,2,3,4,5,6,7,8,9]
                }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-csv"> </i>',
                titleAttr: 'CSV',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> </i>',
                titleAttr: 'PDF',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'print',
                className: 'print',
                text: '<i class="fa fa-print"> </i>'
            }
        ],
        'ajax': {
            'url': "Employees/RegisteredData",
            'dataType': 'json',
            'dataSrc': ''
        },
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1 + ".";
                }
            },
            {
                'data': 'nik',
            },
            {
                'data': 'fullName',
                'bSortable': false
            },
            {
                'data': 'phoneNumber',
                'bSortable': false
            },
            {
                'data': 'gender',
                'bSortable': false,
                'render': (data, type, row) => {
                    if (row['gender'] == 0) {
                        return "Male";
                    } else {
                        return "Female";
                    }
                }
                
            },
            {
                'data': 'email',
                'bSortable': false
            },
            {
                'data': 'birthDate',
                'render': (data, type, row) => {
                    var dataGet = new Date(row['birthDate']);
                    return dataGet.toLocaleDateString();
                }
            },
            {
                'data': 'salary',
                'render': function (data, type, row, meta) {
                    return formatRupiah('' + row['salary'], '');
                }
            },
            {
                'data': 'gpa'
            },
            {
                'data': 'degree',
                'bSortable': false
            },
            {
                'data': 'universityName',
                'bSortable': false
            },
            {
                'data': 'nik',
                'bSortable': false,
                'render': function (data, type, row, meta) {
                    return '<button class="fa fa-edit"  value="' + row['nik'] + '" data-toggle="modal" data-target="#editModal"></button>' +
                        '<button class="fa fa-trash"  value="' + row['nik'] + '" onclick="Delete(this.value)" data-toggle="modal" data-target="#delete"></button>';
                }
            }
        ]
    });
});

/*================================Add Univ Option================================*/
$.ajax({
    'url': "https://localhost:44347/api/Universities",
}).done((result) => {
    text = "<option selected disabled value=\"\">Choose...</option>";
    $.each(result, function (key, val) {
        text += `<option value="${val.universityID}">${val.universityName}</option>`;
    });
    $("#univ").html(text);
}).fail((error) => {
    console.log(error);
});

/*================================Add Education Option================================*/
/*$('#univ').change(function () {
    let univ = $(this).val();
    $.ajax({
        'url': 'https://localhost:44347/API/educations',
    }).done((result) => {
        text = "<option selected disabled value=\"\">Choose...</option>";
        $.each(result.success, function (key, val) {
            if (univ == val.universityID) {
                text += `<option value="${val.degree}">${val.degree}</option>`;
            }
        });
        $("#degree").html(text);
    }).fail((error) => {
        console.log(error);
    });
});*/

/*================================Get Row DataTable================================*/
$('#tableEmployee').on('click', '.fa-edit', function () {
    let rowData = $('#tableEmployee').DataTable().row($(this).closest('tr')).data();
    console.log(rowData);
    Show(rowData);
});

/*================================Show Row Data in Modal================================*/
function Show(data) {
    let gender;
    if (data.gender == "Male") {
        gender = 0;
    }
    else {
        gender = 1;
    }
    const name = data.fullName;
    const [firstName, lastName] = name.split(' ');
    $("#nikedit").val(data.nik);
    $("#firstNameedit").val(firstName);
    $("#lastNameedit").val(lastName);
    $("#emailedit").val(data.email);
    parseInt($("#salaryedit").val(data.salary));
    if (gender == 1) {
        $("#genderedit1").val(gender).prop('checked', true);
    } else {
        $("#genderedit").val(gender).prop('checked', true);
    }
    $("#birthDateedit").val(new Date(data.birthDate + "Z").toISOString().substring(0, 10));
    $("#phoneNumberedit").val(data.phoneNumber);
}

/*================================Edit Employee================================*/
function Edit() {
    var obj = new Object();
    obj.nik = $("#nikedit").val();
    obj.FirstName = $("#firstNameedit").val();
    obj.LastName = $("#lastNameedit").val();
    obj.Email = $("#emailedit").val();
    obj.Salary = parseInt($("#salaryedit").val());
    obj.Gender = parseInt($("input[name=genderedit]:checked").val());
    obj.BirthDate = $("#birthDateedit").val();
    obj.Phone = $("#phoneNumberedit").val();
    $.ajax({
        url: "Employees/Put",
        type: "PUT",
        data: obj,
    }).done((result) => {
        console.log(obj);
        Swal.fire({
            icon: 'success',
            title: result.message,
        });
        table.ajax.reload(null, false);
        chartGender();
        chartUniversity();
        chartSalary();
        $('body').removeClass('modal-open');
        $('#editModal').modal('hide');
        $('.modal-backdrop').remove();
    }).fail((error) => {
        console.log(obj);
        Swal.fire({
            icon: 'error',
            title: 'Edit Gagal',
            text: error.responseJSON.message,
        });
    });
}

/*================================Register================================*/
function Register() {
    var obj = new Object();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Email = $("#email").val();
    obj.Salary = parseInt($("#salary").val());
    obj.Gender = parseInt($("input[name=gender]:checked").val());
    obj.BirthDate = $("#birthDate").val();
    obj.Phone = $("#phone").val();
    obj.Password = $("#password").val();
    obj.GPA = parseFloat($("#gpa").val());
    obj.Degree = $("#degree").val();
    obj.EducationID = parseInt($("#edu").val());
    obj.UniversityID = parseInt($("#univ").val());
    $.ajax({
        url: "Employees/Register",
        type: "POST",
        data: obj,
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: result,
        });
        table.ajax.reload(null, false);
        chartGender();
        chartUniversity();
        chartSalary();
        $('body').removeClass('modal-open');
        $('#regisModal').modal('hide');
        $('.modal-backdrop').remove();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Registrasi Failed',
            text: error.responseJSON.message,
        });
    });
}

/*================================Validation Register================================*/
window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('needs-validation');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                Register();
            }
            form.classList.add('was-validated');
        });
    }
});

/*================================Validation Edit================================*/
window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('needs-valid');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                Edit();
            }
            form.classList.add('was-validated');
        });
    }
});

/*================================Delete Employee================================*/
function Delete(nik) {
    Swal.fire({
        title: 'Yakin ingin dihapus',
        text: "Data akan dihapus dari database",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yakin',
        cancelButtonText: 'Batal',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "Employees/Delete/" + nik,
                type: "DELETE",
                crossDomain: true,
            }).done((result) => {
                Swal.fire(
                    'Berhasil',
                    result.messageResult,
                    'success'
                )
                table.ajax.reload();
            }).fail((error) => {
                Swal.fire(
                    'Gagal',
                    'error'
                )
            })
        }
    })
}

/*================================Login Employee================================*/
/*function Login() {
    var obj = new Object();
    obj.Email = $("#inputEmail").val();
    obj.Password = $("#inputPass").val();
    $.ajax({
        url: 'Account/Login',
        type: 'post',
        data: obj,
        traditional: true,
    }).done(result => {
        console.log(result)
        if (result.message == "Login Success") {
            window.location.href = "https://localhost:44319/home"
        } else {
            window.location.href = 'Account/Login'
        }
    }).fail(error => {
        console.log(error)
    });
}*/
/*function LoginEmployee() {
    var obj = new Object();
    var valueLog = $("#inputEmail").val();
    if (validateEmail(valueLog) == true) {
        obj.Email = valueLog;
    } else {
        obj.Phone = valueLog;
    }

    obj.Password = $("#inputPass").val();
    $.ajax({
        url: "",
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json',
        data: JSON.stringify(obj),
        success: function (respone) {
            if (respone == null) {
                Swal.fire(
                    'Email / Phone / Password salah',
                    'error'
                )
            } else {
                Swal.fire({
                    tittle: 'Login Berhasil',
                    html:
                        'Mohon tunggu <br>' +
                        '<strong></strong> detik <br>' +
                        'Direct to Dashboard Employee',
                    icon: 'success',
                    timer: 10000,
                    showConfirmButton: false,
                    allowOutsideClick: false,
                    didOpen: () => {
                        timerInterval = setInterval(() => {
                            Swal.getHtmlContainer().querySelector('strong')
                                .textContent = (Swal.getTimerLeft() / 1000)
                                    .toFixed(0)
                        }, 100)
                    },
                    willClose: () => {
                        clearInterval(timerInterval)
                        window.location.href = '';
                    }
                })
            }
        },
        error: function (respone) {
            console.log("error : " + JSON.stringify(respone));
            Swal.fire(
                'Sepertinya Terjadi Kesalahan',
                'error'
            )
        }
    });
}

function validateEmail(input) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(input)) {
        return (true)
    }
    return (false)
}*/

/*================================Chart Donut================================*/
function chartGender() {
    male = 0;
    female = 0;
    jQuery.ajax({
        url: 'Employees/RegisteredData',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.gender == 1) {
                    female++;
                }
                else {
                    male++;
                }
            });
            var options = {
                chart: {
                    type: 'donut',
                    size: '50%',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []
                        },
                        export: {
                            csv: {
                                filename: undefined,
                                columnDelimiter: ',',
                                headerCategory: 'category',
                                headerValue: 'value',
                                dateFormatter(timestamp) {
                                    return new Date(timestamp).toDateString()
                                }
                            },
                            svg: {
                                filename: undefined,
                            },
                            png: {
                                filename: undefined,
                            }
                        },
                        autoSelected: 'zoom'
                    },
                },
                dataLabels: {
                    enabled: true
                },
                title: {
                    text: 'Perbandingan Gender',
                    align: 'left',
                    margin: 10,
                    offsetX: 0,
                    offsetY: 0,
                    floating: false,
                    style: {
                        fontSize: '14px',
                        fontWeight: 'bold',
                        fontFamily: undefined,
                        color: '#263238'
                    },
                },
                series: [male, female],
                labels: ['Male', 'Female'],
                noData: {
                    text: 'Loading...'
                }
            }
            var chart = new ApexCharts(document.querySelector("#chartGender"), options);
            chart.render();
        },
        async: false
    })
}
/*================================Chart Donut================================*/
function chartSalary() {
    upper = 0;
    mid = 0;
    jQuery.ajax({
        url: 'Employees/RegisteredData',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.salary >= 15000000) {
                    upper++;
                }
                else {
                    mid++;
                }
            });
            var options = {
                chart: {
                    type: 'donut',
                    size: '50%',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []
                        },
                        export: {
                            csv: {
                                filename: undefined,
                                columnDelimiter: ',',
                                headerCategory: 'category',
                                headerValue: 'value',
                                dateFormatter(timestamp) {
                                    return new Date(timestamp).toDateString()
                                }
                            },
                            svg: {
                                filename: undefined,
                            },
                            png: {
                                filename: undefined,
                            }
                        },
                        autoSelected: 'zoom'
                    },
                },
                dataLabels: {
                    enabled: true
                },
                title: {
                    text: 'Salary Rate',
                    align: 'left',
                    margin: 10,
                    offsetX: 0,
                    offsetY: 0,
                    floating: false,
                    style: {
                        fontSize: '14px',
                        fontWeight: 'bold',
                        fontFamily: undefined,
                        color: '#263238'
                    },
                },
                series: [upper, mid],
                labels: ['Gaji Di Atas 15jt', 'Gaji Di Bawah 15jt'],
                noData: {
                    text: 'Loading...'
                }
            }
            var chart = new ApexCharts(document.querySelector("#chartSalary"), options);
            chart.render();
        },
        async: false
    })
}
/*================================Chart Bar================================*/
function chartUniversity() {
    let universityA = null;
    let universityB = null;
    let universityC = null;
    jQuery.ajax({
        url: 'Employees/RegisteredData',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.universityName == "Telkom University") {
                    universityA++;
                } else if (val.universityName == "Universitas Ibnu Khaldun") {
                    universityB++;
                } else {
                    universityC++;
                }
            });
            var options = {
                chart: {
                    type: 'bar',
                    size: '50%'
                },
                series: [{
                    name: 'Employee',
                    data: [universityA, universityB, universityC]
                }],
                title: {
                    text: 'Jumlah Individu per Universitas',
                    align: 'left',
                    margin: 10,
                    offsetX: 0,
                    offsetY: 0,
                    floating: false,
                    style: {
                        fontSize: '14px',
                        fontWeight: 'bold',
                        fontFamily: undefined,
                        color: '#263238'
                    },
                },
                dataLabels: {
                    enabled: true
                },
                xaxis: {
                    categories: ["Telkom University", "Universitas Ibnu Khaldun", "Universitas Pakuan"]
                }
            }
            var barChart = new ApexCharts(document.querySelector("#chartUniversity"), options);
            barChart.render();
        },
        async: false
    });
}

/*================================Format Salary================================*/
function formatRupiah(angka, prefix) {
    var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
}





