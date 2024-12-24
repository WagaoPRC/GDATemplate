#!/bin/bash
# BATCH PARA GERAR VERSAO:
# @Author: Everton 
# ATUALIZA O CODIGO
# INCREMENTA A VERSAO
# CRIAR TAG
# COMMIT
# PUSH

ARQUIVO_VERSAO="VERSION_TAG.txt"
NEW_VERSION=""

startscript(){
	clear
	echo ####################################################################
	echo "Iniciando processo de versionamento"
}

gitpull(){	
	echo ####################################################################
	echo "ATUALIZANDO O PROJETO LOCAL GITLAB REMOTE-->LOCAL"
	git pull
}

getversion(){
	VERSION=`grep -v "#" $ARQUIVO_VERSAO | awk -F . '{print $1}'`.`grep -v "#" $ARQUIVO_VERSAO | awk -F . '{print $2}'`.`grep -v "#" $ARQUIVO_VERSAO | awk -F . '{print $3}'`	
	X=`grep -v "#" $ARQUIVO_VERSAO | awk -F . '{print $4}'`
	echo "OLD VERSION: $VERSION.$X"
	X=$(($X+1))
	NEW_VERSION="$VERSION.$X"
	echo "TAG: $NEW_VERSION"
	echo $NEW_VERSION > "$ARQUIVO_VERSAO"
	echo "NEW VERSION: $NEW_VERSION"
}

updaterepository(){
	git tag -a $NEW_VERSION -m $NEW_VERSION
	git add .
	git commit -am $NEW_VERSION
	git push origin $NEW_VERSION
	git push
}

startscript
gitpull
getversion
updaterepository


#git pull
#git add .
#git tag -a $NEW_VERSION -m 'version $NEW_VERSION'
#git commit -am $NEW_VERSION
#git push origin $NEW_VERSION
#git push
