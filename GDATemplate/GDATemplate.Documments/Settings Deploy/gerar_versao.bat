:: BATCH PARA GERAR VERSAO:
:: @Author: Everton 
::
:: ATUALIZA O CODIGO
:: INCREMENTA A VERSAO
:: CRIAR TAG
:: COMMIT 
:: PUSH

@echo off
cls
echo ####################################################################
echo Iniciando processo de versionamento

set ARQUIVO_VERSAO="VERSION_TAG.txt"
echo ####################################################################
echo Arquivo atual: %ARQUIVO_VERSAO%

echo.
echo ####################################################################
set /p VERSAO_ATUAL=<%ARQUIVO_VERSAO%

set ARQUITETURA=0
set FUNCIONALIDADE=0
set CORRECAO_GRANDE=0
set CORRECAO_PEQUENA=0

echo.-- ATUALIZANDO REPOSITORIO LOCAL, OBSERVAR ERROS
:GIT_PULL

echo.-- DADOS SOBRE A VERSAO ATUAL
echo Versao atual: %VERSAO_ATUAL%
for /f "tokens=1,2,3,4 delims=. " %%a in ("%VERSAO_ATUAL%") do set ARQUITETURA=%%a&set FUNCIONALIDADE=%%b&set CORRECAO_GRANDE=%%c&set CORRECAO_PEQUENA=%%d
	echo.ARQUITETURA		:%ARQUITETURA%
	echo.FUNCIONALIDADE		:%FUNCIONALIDADE%
	echo.CORRECAO_GRANDE		:%CORRECAO_GRANDE%
	echo.CORRECAO_PEQUENA	:%CORRECAO_PEQUENA%
	
	echo.
	echo.-- GERANDO NOVA VERSAO
	@set /a CORRECAO_PEQUENA=%CORRECAO_PEQUENA%+1
	echo.ARQUITETURA		:%ARQUITETURA%
	echo.FUNCIONALIDADE		:%FUNCIONALIDADE%
	echo.CORRECAO_GRANDE		:%CORRECAO_GRANDE%
	echo.CORRECAO_PEQUENA	:%CORRECAO_PEQUENA%	
	CALL :GERAR_NOVA_VERSAO	
	CALL :GIT_PUSH %ARQUITETURA%.%FUNCIONALIDADE%.%CORRECAO_GRANDE%.%CORRECAO_PEQUENA%
	CALL :FINALIZADO_PROCESSO
	
EXIT /B %ERRORLEVEL%
:GERAR_NOVA_VERSAO
	echo.
	echo.
	echo ####################################################################
	echo.-- DADOS SOBRE A NOVA VERSAO
	set VERSAO_NOVA=%ARQUITETURA%.%FUNCIONALIDADE%.%CORRECAO_GRANDE%.%CORRECAO_PEQUENA%
	echo.-- NOVA VERSAO DA APP: %VERSAO_NOVA%
	echo.
	echo.-- ATUALIZANDO O ARQUIVO %ARQUIVO_VERSAO%
	echo %VERSAO_NOVA%> %ARQUIVO_VERSAO%	
EXIT /B 0

EXIT /B %ERRORLEVEL%
:GIT_PULL
	echo.
	echo ####################################################################
	echo.-- ATUALIZANDO O PROJETO LOCAL GITLAB REMOTE-->LOCAL
	git pull
EXIT /B 0

EXIT /B %ERRORLEVEL%
:GIT_PUSH
	echo.
	echo ####################################################################
	echo.-- ATUALIZANDO O GITLAB PARA A VERSAO %~1
	echo "TAG: $%~1"
	git tag -a "%~1" -m "%~1"
	git add .
	git commit -m "%~1"
	git push origin "%~1"
	git push
	
EXIT /B 0

EXIT /B %ERRORLEVEL%
:FINALIZADO_PROCESSO
	echo.
	echo ####################################################################
	echo.-- FINALIZADO O PROCESSO DE VERSIONAMENTO
EXIT /B 0	