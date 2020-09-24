cmake_minimum_required(VERSION 3.18)

set(CMAKE_CXX_STANDARD 20)

# ======================================
#              APPLICATION
# ======================================
set(proj_app application)
project(${proj_app})

set(headers_app
    include/application/Checker.h
    include/application/Program.h
    include/GetInfo.h
)

set(sources_app
    source/application/main.cc
    source/application/Checker.cc
    source/application/Program.cc
    source/GetInfo.cc
)

add_executable(${proj_app}
    ${headers_app}
    ${sources_app}
)

target_include_directories(${proj_app}
    PUBLIC
        "include"
)

# ======================================
#              INSTALLER
# ======================================
set(proj_installer installer)
project(${proj_installer})

set(headers_installer
    include/installer/GenerateKey.h
    include/GetInfo.h
)

set(sources_installer
    source/installer/main.cc
    source/installer/GenerateKey.cc
    source/GetInfo.cc
)

add_executable(${proj_installer}
    ${headers_installer}
    ${sources_installer}
)

target_include_directories(${proj_installer}
    PUBLIC
        "include"
)
