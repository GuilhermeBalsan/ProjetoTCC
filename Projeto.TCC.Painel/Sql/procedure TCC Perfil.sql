ALTER PROCEDURE RelatorioQuestionario 
@QuestionarioID INT
as
BEGIN

DECLARE @CountPerguntas decimal(4,2)
DECLARE @Perfil varchar(255)
DECLARE @PivotAux varchar(MAX) = ''
DECLARE @PivotSelect varchar(MAX) = ''
DECLARE @SqlCmd varchar(max) = ''

IF OBJECT_ID('tempdb..#tmpPeso') IS NOT NULL
    DROP TABLE #tmpPeso

IF OBJECT_ID('tempdb..#TmpOrdem') IS NOT NULL
    DROP TABLE #TmpOrdem

IF OBJECT_ID('tempdb..##Detalhes') IS NOT NULL
    DROP TABLE ##Detalhes

CREATE TABLE ##Detalhes(
	[Ordem] int identity, 
	[QuestionarioId] [int] NOT NULL,
	[Questionario] [nvarchar](max) NOT NULL,
	[CandidatoId] [int] NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[PerfilId] [int] NOT NULL,
	[Perfil] [nvarchar](max) NOT NULL,
	[Representacao] [decimal](22, 6) NULL
)

--Quantidade total de perguntas do formulário
SELECT @CountPerguntas = count(*) FROM Questionarios a 
inner join Perguntas b
on a.Id = b.QuestionarioId
where a.Id = @QuestionarioID

--Ordenar os resultados de acordo com o peso do Questionário e de quem teve mais pontuação desse peso
;with tmp as
(
select a.QuestionarioId, a.CandidatoId, b.Quantidade, b.PerfilId
from Resultados A
inner join DetalhesResultado b
on a.Id = b.Resultado_Id
where QuestionarioId = @QuestionarioID
)
select a.*, 
b.Peso
into #tmpPeso
from tmp a
left join PesosQuestionarios b
on a.PerfilId = b.PerfilId
order by b.Peso desc, a.Quantidade desc

insert into ##Detalhes
--Deixar tabela com detalhes dos Candidatos, já que está ordenada, achando as representações
select
b.Id as QuestionarioId, 
b.Nome as Questionario,
d.Id as CandidatoId,
d.Nome, 
c.Id as PerfilId,
c.Titulo as Perfil,
(a.Quantidade/@CountPerguntas)*100 as Representacao
from #tmpPeso a
inner join Questionarios b
on b.Id = a.QuestionarioId
inner join Perfis c
on c.Id = a.PerfilId
inner join Candidatos d
on d.Id = a.CandidatoId
order by a.Peso desc, a.Quantidade desc

SELECT MIN(ORDEM) AS ORDEM, NOME
INTO #TmpOrdem 
FROM ##Detalhes
GROUP BY NOME
ORDER BY MIN(ORDEM) ASC

DECLARE PerfilCursor CURSOR FOR   
select distinct Perfil from ##Detalhes  
  
OPEN PerfilCursor 

FETCH NEXT FROM PerfilCursor  
INTO @Perfil
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
SET @PivotAux = @PivotAux + '[' + @Perfil + '],'
SET @PivotSelect = @PivotSelect + 'CONVERT(VARCHAR, CAST(ISNULL(' + @Perfil + ', 0) AS DECIMAL(18,2))) + ''%'' AS ' + @Perfil + ', '
FETCH NEXT FROM PerfilCursor  
    INTO @Perfil 
END   
CLOSE PerfilCursor;  
DEALLOCATE PerfilCursor;  

SET @PivotAux = SUBSTRING(@PivotAux, 1, (LEN(@PivotAux) - 1))
SET @PivotSelect = SUBSTRING(@PivotSelect, 1, (LEN(@PivotSelect) - 1))


SET @SqlCmd = 
';WITH TMP AS
(
select CandidatoId, QuestionarioId, NOME, ' + @PivotSelect + ' 
from 
(
  select CandidatoId, QuestionarioId, Nome, Perfil, Representacao
  from ##Detalhes
) src
pivot
(
  sum(Representacao)
  for Perfil in (' + @PivotAux + ')
) piv
)
SELECT b.ORDEM, a.* FROM TMP A
INNER JOIN #TmpOrdem B
ON A.Nome = B.Nome
ORDER BY ORDEM ASC'

EXEC (@SqlCmd)




IF OBJECT_ID('tempdb..#tmpPeso') IS NOT NULL
    DROP TABLE #tmpPeso

IF OBJECT_ID('tempdb..#TmpOrdem') IS NOT NULL
    DROP TABLE #TmpOrdem

IF OBJECT_ID('tempdb..##Detalhes') IS NOT NULL
    DROP TABLE ##Detalhes

END
